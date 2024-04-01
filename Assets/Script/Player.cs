using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 인벤토리 관리
    int equipWeaponIndex = -1;
    int iteminfo1 = -1;
    int iteminfo2 = -1;
    int iteminfo3 = -1;

    // 이동방향 및 공격 딜레이
    float haxis;
    float vaxis;
    float fireDelay;

    // 플레이어 상태
    bool wdown;
    bool sdown1;
    bool sdown2;
    bool sdown3;
    bool sdownf1;
    bool sdownf2;
    bool sdownf3;
    bool jdown;
    bool fdown;
    bool idown;
    bool rdown;
    bool gdown;

    bool isdodge;
    bool isFireReady = true;
    bool isswap;
    public bool isreload;
    bool isborder;
    bool isjump;
    bool isdamage;
    public bool isshop;
    public bool isDead;
    bool isbuff;
    bool iscbuff = false;
    public bool isstop;
   
    public float speed;

    // 소지 아이템
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public GameObject[] grenades;
    public int hasGrenade;
    public GameObject[] grenadeObj;
    public Camera followCamera;
    public GameManager manager;
    public GameObject buffEffect;

    public int ammo;
    public int coin;
    public int health;
    public int score;
    public int defens;
    
    public int maxammo;
    public int maxcoin;
    public int maxhealth;
    public int maxhasGrenade;

    public bool havef1 = false;
    public bool havef2 = false;
    public bool havef3 = false;


    // 퀘스트 관련 및 버프
    public int Qenemy;
    public int Qgrenade;

    public int enemyAcount;
    public int enemyBcount;
    public int enemyCcount;
    public int enemyDcount;

    Color buffcolor;

    public Animator anim;
    Rigidbody rigid;
    MeshRenderer[] meshs;

    Vector3 moveVec;
    Vector3 dodgeVec;

    public GameObject nearObject;
    public Weapon equipWeapon;

    private List<int> GrenadeList = new List<int> { };

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        meshs = GetComponentsInChildren<MeshRenderer>();
    }
    void Start()
    {
        maxhealth += DataManager.instance.nowPlayer.MaxHp;
        health = maxhealth;
        ammo += DataManager.instance.nowPlayer.Ammo;
        coin += DataManager.instance.nowPlayer.Gold;
        hasWeapons[0] = DataManager.instance.nowPlayer.Weapon1;
        hasWeapons[1] = DataManager.instance.nowPlayer.Weapon2;
        hasWeapons[2] = DataManager.instance.nowPlayer.Weapon3;
    }

    private void FixedUpdate()
    {
        StopToWall();
        FreezeRotation();
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update() // 업데이트문이 더럽지 않도록 함수 기능별로 정리
    {
        GetInput();
        Move();
        Turn();
        Dodge();
        Interation();
        Swap();
        Attack();
        Reload();
        Grenade();
        Usepotion();
    }

    void GetInput() // 키 입력 함수들
    {
        haxis = Input.GetAxisRaw("Horizontal"); // GetAxis -> Axis값을 정수로 반환
        vaxis = Input.GetAxisRaw("Vertical");
        wdown = Input.GetButton("Walk");
        jdown = Input.GetButtonDown("Jump");
        idown = Input.GetButtonDown("Interation");
        sdown1 = Input.GetButtonDown("Swap1");
        sdown2 = Input.GetButtonDown("Swap2");
        sdown3 = Input.GetButtonDown("Swap3");
        sdownf1 = Input.GetButtonDown("SlotF1");
        sdownf2 = Input.GetButtonDown("SlotF2");
        sdownf3 = Input.GetButtonDown("SlotF3");
        fdown = Input.GetButton("Fire1");
        gdown = Input.GetButtonDown("Fire2");
        rdown = Input.GetButtonDown("Reload");
    }

    private void Move()
    {
        moveVec = new Vector3(haxis, 0, vaxis).normalized; // normalized : 방향 값이 1로 보정된 벡터

        if (isdodge) // 구르고 있을 때
            moveVec = dodgeVec;

        if (isswap || !isFireReady || isreload || isDead || isstop) // 못 움직이는 상태
            moveVec = Vector3.zero;

        if(!isborder)
            transform.position += moveVec * speed * (wdown ? 0.3f : 1f) * Time.deltaTime;


        anim.SetBool("Run", moveVec != Vector3.zero);
        anim.SetBool("Walk", wdown);


        if (!isdodge && speed != 15 && !isbuff) // 평상시 이동 속도 제한
            speed = 15;
    }

    void Turn() // 캐릭터의 시점
    {
        // 키보드 회전 
        transform.LookAt(transform.position + moveVec); // LookAt : 지정된 벡터를 향해 회전시켜주는 함수

        // 마우스 회전
        if (fdown && !isdodge && !isDead)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);  // ScreenPointToRay 스크린에서 월드로 Ray쏘는 함수
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100)) // out : return처럼 반환값을 주어진 변수에 저장하는 키워드
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }

    void Dodge() // 구르기
    {
        if (jdown && moveVec != Vector3.zero && !isjump && !isdodge && !isswap && !isDead)
        {
            dodgeVec = moveVec;
            speed *= 2; // 순간적인 이동속도
            isdodge = true;
            anim.SetTrigger("Dodge");

            Invoke("DodgeOut", 0.5f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isdodge = false;
    }

    void Swap() // 무기 스왑
    {
        if(sdown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) // 1번을 눌렀을 시 
        {
            return;
        }
        if (sdown2 && (!hasWeapons[1] || equipWeaponIndex == 1)) // 2번을 눌렀을 시
        {
            return;
        }
        if (sdown3 && (!hasWeapons[2] || equipWeaponIndex == 2)) // 3번을 눌렀을 시
        {
            return;
        }

        int weaponIndex = -1; // 인덱스 기본값

        if (sdown1)
            weaponIndex = 0;
        if (sdown2)
            weaponIndex = 1;
        if (sdown3)
            weaponIndex = 2;

        if ((sdown1 || sdown2 || sdown3) && !isjump && !isdodge && !isDead) // 무기 스왑 함수
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>(); // 웨폰 인덱스로 인한 무기 엑티브 활성화
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("Swap");
            isswap = true;

            Invoke("Swapout", 0.4f);
        }
    }

    void Swapout()
    {
        isswap = false;
    }

    void Interation() // 상호작용키 입력
    {
        if(idown && nearObject != null && !isjump && !isdodge && !isDead) // E ( 상호작용 ) 버튼을 눌렀을 경우 구별합니다.
        {
            if(nearObject.tag == "Weapon") // 무기일 경우
            {
                Item item = nearObject.GetComponent<Item>();
                int weponIndex = item.value; // 아이템 벨류값에 따른 인덱스 지정
                hasWeapons[weponIndex] = true; // 소지여부 true 값 반환

                // 아이템 저장
                if (weponIndex == 0)
                    DataManager.instance.nowPlayer.Weapon1 = true;
                else if (weponIndex == 1)
                    DataManager.instance.nowPlayer.Weapon2 = true;
                else if (weponIndex == 2)
                    DataManager.instance.nowPlayer.Weapon3 = true;
                Destroy(nearObject); // 필드에 있던 아이템 삭제
            }
            else if (nearObject.tag == "Shop") // 상점일 경우
            {
                Shop shop = nearObject.GetComponent<Shop>(); // 상점 열기
                shop.Enter(this);
                isshop = true; // isshop 상태 활성화
            }
            else if (nearObject.tag == "Potion") // 포션일 경우
            {
                // 포션 F1, F2, F3칸 아이템 존재 여부 확인 및 추가
                if (!havef1) 
                {
                    Item item = nearObject.GetComponent<Item>();
                    manager.potioncontrol(item.value);
                    iteminfo1 = item.value;
                    Destroy(nearObject);
                    havef1 = true;
                }
                else if (havef1 && !havef2)
                {
                    Item item = nearObject.GetComponent<Item>();
                    manager.potioncontrol(item.value);
                    iteminfo2 = item.value;
                    Destroy(nearObject);
                    havef2 = true;
                }
                else if (havef1 && havef2 && !havef3)
                {
                    Item item = nearObject.GetComponent<Item>();
                    manager.potioncontrol(item.value);
                    iteminfo3 = item.value;
                    Destroy(nearObject);
                    havef3 = true;
                }
                else
                    return;
            }
        }

        if (hasWeapons[0] == true && MainQuest.Instance.QuestOn && MainQuest.Instance.QuestList[MainQuest.Instance.QuestValue].QuestID == 1)
        {
            MainQuest.Instance.isClear = true;
            Debug.Log("퀘스트클리어해머");
        }
    }

    void Attack() // 공격
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime; // 공격 딜레이
        isFireReady = equipWeapon.rate < fireDelay;

        if(fdown && isFireReady && !isdodge && !isstop &&!isswap && !isshop && !isDead) // 공격 키를 누를시
        {
            equipWeapon.Use(); // 들고있는 무기의 Use()함수 사용
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "Swing" : "Shot");
            fireDelay = 0;
        }
    }

    void Reload() // 재장전
    {
        if (equipWeapon == null) // 소지하지 않을 경우 리턴
            return;

        if (equipWeapon.type == Weapon.Type.Melee) // 헤머일 경우 리턴
            return;

        if (ammo == 0) // 총알이 없는 경우 리턴
            return;

        if(rdown && !isjump && !isswap && !isdodge && isFireReady && !isDead) // 재장전 버튼을 누를시
        {
            anim.SetTrigger("Reload");
            isreload = true;
            SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[5];
            SoundManager.instance.Effect_Sound.Play();
            Invoke("ReloadOut", 3f);

        }
    }

    void ReloadOut() // 재장전 끝날 시
    {
        // 해당 총알 차감
        int reAmmo = ammo < equipWeapon.maxammo ? ammo : equipWeapon.maxammo;
        equipWeapon.curammo = reAmmo;
        ammo -= reAmmo;
        DataManager.instance.nowPlayer.Ammo -= reAmmo;
        isreload = false;
    }

    void StopToWall() // 벽 뚫기 방지
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isborder = Physics.Raycast(transform.position, transform.forward , 5, LayerMask.GetMask("Wall"));
    }
    
    void Grenade() // 수류탄
    {
        if (hasGrenade == 0) // 수류탄을 소지하지 않을 시
            return;

        if(gdown && !isreload && !isswap && !isDead) // 우클릭을 누르면
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);  // ScreenPointToRay 스크린에서 월드로 Ray쏘는 함수 수류탄 던질 위치
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100)) // out : return처럼 반환값을 주어진 변수에 저장하는 키워드
            {
                Vector3 nextVec = rayHit.point - transform.position; // 수류탄이 지점까지 던져지는 지점 계산
                nextVec.y = 10;

                // 생성
                GameObject instantGrenade = Instantiate(grenadeObj[GrenadeList[0]], transform.position, transform.rotation); // GrenadeList에있는 수류탄을 생성합니다.
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>(); // 그 수류탄의 컴포넌트를 가져온다.
                rigidGrenade.AddForce(nextVec, ForceMode.Impulse); // 수류탄의 던져지면서의 물리현상을 구현
                rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);
                GrenadeList.RemoveAt(0); // 리스트 삭제
                hasGrenade--; // 수류탄 갯수 소모
                Qgrenade--; // 퀘스트를 진행중일 때 갯수 소모
                childoff(grenades[hasGrenade]); // 회전하는 수류탄의 액티브 끄기

                if (MainQuest.Instance.QuestOn && MainQuest.Instance.QuestList[MainQuest.Instance.QuestValue].QuestID == 2)
                {
                    MainQuest.Instance.Grenade_Count++;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon" || other.tag == "Potion")
            nearObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null)
            return;

        if (other.tag == "Weapon")
            nearObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item") // 아이템에 닿으면
        {
            Item item = other.GetComponent<Item>();
            switch (item.type) // 아이템 타입을 구별합니다
            {
                case Item.Type.Ammo: // 총알이면 총알증가
                    ammo += item.value;
                    DataManager.instance.nowPlayer.Ammo += item.value;
                    if (ammo > maxammo)
                        ammo = maxammo;
                    break;
                case Item.Type.Coin: // 코인이면 골드 증가
                    if(iscbuff) // 버프 상태이면 두배
                    {
                        coin += item.value * 2;
                        DataManager.instance.nowPlayer.Gold += item.value * 2;
                    }
                    else
                    {
                        coin += item.value;
                        DataManager.instance.nowPlayer.Gold += item.value;
                    }
                    if (coin > maxcoin)
                        coin = maxcoin;
                    SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[11];
                    SoundManager.instance.Effect_Sound.Play();
                    break;
                case Item.Type.Heart: // 하트면 현재 HP 증가
                    health += item.value;
                    if (health > maxhealth)
                        health = maxhealth;
                    break;
                case Item.Type.Grenade: // 수류탄이면 수류탄 증가
                    if (hasGrenade == maxhasGrenade)
                        return;
                    //수류탄 종류 판별 후 넣기
                    GrenadeList.Add(item.Grenadevalue);
                    //위치 grenades에 0, 1, 2, 3 중 0이 켜지고 item.value값으로 차일드 키기
                    grenades[hasGrenade].SetActive(true);
                    grenadeargorizm();
                    grenades[0].transform.GetChild(item.Grenadevalue).gameObject.SetActive(true);
                    hasGrenade += item.value; // 수류탄 갯수
                    break;
            }
            Destroy(other.gameObject);
        }
        else if(other.tag == "EnemyBullet") // 상대의 공격이면
        {
            if (!isdamage) // 무적상태가 아닐 때
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();

                if(defens < enemyBullet.damage) // 상대의 데미지와 내 방어력 계산
                {
                    health -= (enemyBullet.damage - defens);
                }
                bool isBossAtk = other.name == "Boss Melee Area";
                StartCoroutine(OnDamage(isBossAtk));
            }

            if (other.GetComponent<Rigidbody>() != null)
                Destroy(other.gameObject);
        }
    }

    void grenadeargorizm() // 수류탄 알고리즘
    {
        if (grenades[2].transform.GetChild(0).gameObject.activeSelf || grenades[2].transform.GetChild(1).gameObject.activeSelf || grenades[2].transform.GetChild(2).gameObject.activeSelf)
        {
            if (grenades[2].transform.GetChild(0).gameObject.activeSelf)
            {
                grenades[2].transform.GetChild(0).gameObject.SetActive(false);
                grenades[3].transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (grenades[2].transform.GetChild(1).gameObject.activeSelf)
            {
                grenades[2].transform.GetChild(1).gameObject.SetActive(false);
                grenades[3].transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (grenades[2].transform.GetChild(2).gameObject.activeSelf)
            {
                grenades[2].transform.GetChild(2).gameObject.SetActive(false);
                grenades[3].transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        if (grenades[1].transform.GetChild(0).gameObject.activeSelf || grenades[1].transform.GetChild(1).gameObject.activeSelf || grenades[1].transform.GetChild(2).gameObject.activeSelf)
        {
            if (grenades[1].transform.GetChild(0).gameObject.activeSelf)
            {
                grenades[1].transform.GetChild(0).gameObject.SetActive(false);
                grenades[2].transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (grenades[1].transform.GetChild(1).gameObject.activeSelf)
            {
                grenades[1].transform.GetChild(1).gameObject.SetActive(false);
                grenades[2].transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (grenades[1].transform.GetChild(2).gameObject.activeSelf)
            {
                grenades[1].transform.GetChild(2).gameObject.SetActive(false);
                grenades[2].transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        if (grenades[0].transform.GetChild(0).gameObject.activeSelf || grenades[0].transform.GetChild(1).gameObject.activeSelf || grenades[0].transform.GetChild(2).gameObject.activeSelf)
        {
            if (grenades[0].transform.GetChild(0).gameObject.activeSelf)
            {
                grenades[0].transform.GetChild(0).gameObject.SetActive(false);
                grenades[1].transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (grenades[0].transform.GetChild(1).gameObject.activeSelf)
            {
                grenades[0].transform.GetChild(1).gameObject.SetActive(false);
                grenades[1].transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (grenades[0].transform.GetChild(2).gameObject.activeSelf)
            {
                grenades[0].transform.GetChild(2).gameObject.SetActive(false);
                grenades[1].transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    

    IEnumerator OnDamage(bool isBossAtk) // 데미지를 입는 방식
    {
        SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[4];
        SoundManager.instance.Effect_Sound.Play();

        isdamage = true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow; // 피격시 노랑색 변화
        }

        if (isBossAtk)
            rigid.AddForce(transform.forward * -25, ForceMode.Impulse); // 데미지를 받을 시 밀려나는 현상

        if (health <= 0 && !isDead)
            OnDie();

        yield return new WaitForSeconds(1f);

        isdamage = false;

        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white; // 다시 색깔 기본값
        }

        if (isBossAtk)
            rigid.velocity = Vector3.zero; 
    }

    void childoff(GameObject obj) // 몸 주변 수류탄 엑티브 끄기
    {
        if (obj.transform.GetChild(0).gameObject.activeSelf)
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (obj.transform.GetChild(1).gameObject.activeSelf)
        {
            obj.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (obj.transform.GetChild(2).gameObject.activeSelf)
        {
            obj.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    void Usepotion() // 포션 사용시
    {
        ParticleSystem buf = buffEffect.GetComponent<ParticleSystem>();
        var main = buf.main;
        // 어떤 키를 눌렀는지 알아내기
        if (havef1 && sdownf1 && !isbuff) // F1인 경우
        {
            Potion(iteminfo1); // F1에 담긴 포션의 능력을 플레이어에게 부여시킵니다
            havef1 = false; // 소지판별을 False로 바꿉니다
            manager.isitembool1 = false; // 소지하고 있지 않게 합니다.
            main.startColor = buffcolor; // 버프 색깔에 맞게 활성화합니다.
            if (MainQuest.Instance.QuestOn && MainQuest.Instance.QuestList[MainQuest.Instance.QuestValue].QuestID == 2)
            {
                MainQuest.Instance.Potion_Count++;
            }

            SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[13];
            SoundManager.instance.Effect_Sound.Play();
        }

        if (havef2 && sdownf2 && !isbuff)
        {
            Potion(iteminfo2);
            havef2 = false;
            manager.isitembool2 = false;
            main.startColor = buffcolor;

            SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[13];
            SoundManager.instance.Effect_Sound.Play();
        }

        if (havef3 && sdownf3 && !isbuff)
        {
            Potion(iteminfo3);
            havef3 = false;
            manager.isitembool3 = false;
            main.startColor = buffcolor;

            SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[13];
            SoundManager.instance.Effect_Sound.Play();
        }
    }

    void Potion(int value) // 포션 능력 부여 함수
    {
        isbuff = true; // 버프 중첩 상태 방지를 위해
        buffEffect.SetActive(true); // 버프 이펙트를 켜줍니다.
        GameObject hand = GameObject.Find("Weapon Point");
        Weapon Hammer = hand.transform.GetChild(0).gameObject.GetComponent<Weapon>();
        Weapon Handgun = hand.transform.GetChild(1).gameObject.GetComponent<Weapon>();
        Weapon Subgun = hand.transform.GetChild(2).gameObject.GetComponent<Weapon>();

        switch (value) // 각 능력을 부여해 주기위해 switch로 판별해줍니다.
        {
            case 0:
                speed += 10;
                buffcolor = Color.green;
                break;
            case 1:
                maxhealth += 50;
                buffcolor = Color.red;
                break;
            case 2:
                Hammer.damage += 10;
                Handgun.damage += 7;
                Subgun.damage += 3;
                buffcolor = new Color(1f / 255f, 235f / 255f, 255f / 255f);
                break;
            case 3:
                Hammer.rate -= 0.2f;
                Handgun.rate -= 0.1f;
                Subgun.rate -= 0.04f;
                buffcolor = Color.gray;
                break;
            case 4:
                iscbuff = true;
                buffcolor = new Color(255f / 255f, 1f / 255f, 221f / 255f);
                break;
        }
        StopCoroutine(Buffcontrol(value));
        StartCoroutine(Buffcontrol(value));
    }

    IEnumerator Buffcontrol(int value)
    {
        yield return new WaitForSeconds(10f);
        GameObject hand = GameObject.Find("Weapon Point");
        Weapon Hammer = hand.transform.GetChild(0).gameObject.GetComponent<Weapon>();
        Weapon Handgun = hand.transform.GetChild(1).gameObject.GetComponent<Weapon>();
        Weapon Subgun = hand.transform.GetChild(2).gameObject.GetComponent<Weapon>();
        switch (value)
        {
            case 0:
                speed -= 10;
                break;
            case 1:
                maxhealth -= 50;
                break;
            case 2:
                Hammer.damage -= 10;
                Handgun.damage -= 7;
                Subgun.damage -= 3;
                break;
            case 3:
                Hammer.rate = 0.7f;
                Handgun.rate = 0.4f;
                Subgun.rate = 0.15f;
                break;
            case 4:
                iscbuff = false;
                break;
        }
        isbuff = false;
        buffEffect.SetActive(false);
    }

    void OnDie()
    {
        anim.SetTrigger("Die");
        isDead = true;
        manager.GameOver();
    }

    public void PlayerSave()
    {
        DataManager.instance.nowPlayer.MaxHp = maxhealth;
        DataManager.instance.nowPlayer.Ammo = ammo;
        DataManager.instance.nowPlayer.Gold = coin;
        DataManager.instance.nowPlayer.Defens = defens;
    }

}
