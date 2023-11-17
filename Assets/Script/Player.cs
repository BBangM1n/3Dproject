using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    int equipWeaponIndex = -1;
    int iteminfo1 = -1;
    int iteminfo2 = -1;
    int iteminfo3 = -1;

    float haxis;
    float vaxis;
    float fireDelay;

    bool wdown;
    bool sdown1;
    bool sdown2;
    bool sdown3;
    bool sdown4;
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
    bool isreload;
    bool isborder;
    bool isjump;
    bool isdamage;
    bool isshop;
    bool isDead;
    bool isbuff;
    bool iscbuff = false;
    

    public float speed;
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
    
    public int maxammo;
    public int maxcoin;
    public int maxhealth;
    public int maxhasGrenade;

    public bool havef1 = false;
    public bool havef2 = false;
    public bool havef3 = false;

    public int Qenemy;
    public int Qgrenade;
    public int enemyAcount;
    public int enemyBcount;
    public int enemyCcount;
    public int enemyDcount;

    Color buffcolor;

    Animator anim;
    Rigidbody rigid;
    MeshRenderer[] meshs;

    Vector3 moveVec;
    Vector3 dodgeVec;

    GameObject nearObject;
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
        Jump();
        Dodge();
        Interation();
        Swap();
        Attack();
        Reload();
        Grenade();
        Usepotion();
    }

    void GetInput()
    {
        haxis = Input.GetAxisRaw("Horizontal"); // GetAxis -> Axis값을 정수로 반환
        vaxis = Input.GetAxisRaw("Vertical");
        wdown = Input.GetButton("Walk");
        jdown = Input.GetButtonDown("Jump");
        idown = Input.GetButtonDown("Interation");
        sdown1 = Input.GetButtonDown("Swap1");
        sdown2 = Input.GetButtonDown("Swap2");
        sdown3 = Input.GetButtonDown("Swap3");
        sdown4 = Input.GetButtonDown("Swap4");
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

        if (isdodge)
            moveVec = dodgeVec;

        if (isswap || !isFireReady || isreload || isDead)
            moveVec = Vector3.zero;

        if(!isborder)
            transform.position += moveVec * speed * (wdown ? 0.3f : 1f) * Time.deltaTime;


        anim.SetBool("Run", moveVec != Vector3.zero);
        anim.SetBool("Walk", wdown);


        if (!isdodge && speed != 15 && !isbuff)
            speed = 15;
    }

    void Turn()
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

    void Jump()
    {
        if (jdown && moveVec == Vector3.zero && !isjump && !isdodge && !isswap && !isDead)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            isjump = true;
            anim.SetBool("isJump", true);
            anim.SetTrigger("Jump");
        }
    }

    void Dodge()
    {
        if (jdown && moveVec != Vector3.zero && !isjump && !isdodge && !isswap && !isDead)
        {
            dodgeVec = moveVec;
            speed *= 2;
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

    void Swap()
    {
        if(sdown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
        {
            return;
        }
        if (sdown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
        {
            return;
        }
        if (sdown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
        {
            return;
        }
        if (sdown4 && (!hasWeapons[3] || equipWeaponIndex == 3))
        {
            return;
        }

        int weaponIndex = -1;
        if (sdown1)
            weaponIndex = 0;
        if (sdown2)
            weaponIndex = 1;
        if (sdown3)
            weaponIndex = 2;
        if (sdown4)
            weaponIndex = 3;

        if ((sdown1 || sdown2 || sdown3 || sdown4) && !isjump && !isdodge && !isDead)
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
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

    void Interation()
    {
        if(idown && nearObject != null && !isjump && !isdodge && !isDead)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weponIndex = item.value;
                hasWeapons[weponIndex] = true;

                Destroy(nearObject);
            }
            else if (nearObject.tag == "Shop")
            {
                Shop shop = nearObject.GetComponent<Shop>();
                shop.Enter(this);
                isshop = true;
            }
            else if (nearObject.tag == "Potion")
            {
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
    }

    void Attack()
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(fdown && isFireReady && !isdodge && !isswap && !isshop && !isDead)
        {
            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "Swing" : "Shot");
            fireDelay = 0;
        }
    }

    void Reload()
    {
        if (equipWeapon == null)
            return;

        if (equipWeapon.type == Weapon.Type.Melee)
            return;

        if (ammo == 0)
            return;

        if(rdown && !isjump && !isswap && !isdodge && isFireReady && !isDead)
        {
            anim.SetTrigger("Reload");
            isreload = true;

            Invoke("ReloadOut", 3f);

        }
    }

    void ReloadOut()
    {
        int reAmmo = ammo < equipWeapon.maxammo ? ammo : equipWeapon.maxammo;
        equipWeapon.curammo = reAmmo;
        ammo -= reAmmo;
        isreload = false;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isborder = Physics.Raycast(transform.position, transform.forward , 5, LayerMask.GetMask("Wall"));
    }
    
    void Grenade()
    {
        if (hasGrenade == 0)
            return;

        if(gdown && !isreload && !isswap && !isDead)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);  // ScreenPointToRay 스크린에서 월드로 Ray쏘는 함수
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100)) // out : return처럼 반환값을 주어진 변수에 저장하는 키워드
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 10;

                // 생성
                GameObject instantGrenade = Instantiate(grenadeObj[GrenadeList[0]], transform.position, transform.rotation);
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                rigidGrenade.AddForce(nextVec, ForceMode.Impulse);
                rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);
                GrenadeList.RemoveAt(0);
                hasGrenade--;
                Qgrenade--;
                childoff(grenades[hasGrenade]);
                //해당 수류탄 이미지 꺼지는 함수
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isjump = false;
            anim.SetBool("isJump", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon" || other.tag == "Shop" || other.tag == "Potion")
            nearObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null)
            return;

        if (other.tag == "Weapon")
            nearObject = null;
        else if (other.tag == "Shop" && isshop)
        {
            Shop shop = nearObject.GetComponent<Shop>();
            if(shop != null)
            {
                shop.Exit();
                isshop = false;
                nearObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxammo)
                        ammo = maxammo;
                    break;
                case Item.Type.Coin:
                    if(iscbuff)
                        coin += item.value * 2;
                    else
                        coin += item.value;
                    if (coin > maxcoin)
                        coin = maxcoin;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxhealth)
                        health = maxhealth;
                    break;
                case Item.Type.Grenade:
                    if (hasGrenade == maxhasGrenade)
                        return;
                    //수류탄 종류 판별 후 넣기
                    GrenadeList.Add(item.Grenadevalue);
                    //위치 grenades에 0, 1, 2, 3 0이 켜지고 item.value값으로 차일드 키기
                    //아마 hasgrenade가 1이상일때 미루는방식 고안해야할듯
                    grenades[hasGrenade].SetActive(true);
                    grenadeargorizm();//함수자리
                    grenades[0].transform.GetChild(item.Grenadevalue).gameObject.SetActive(true);
                    hasGrenade += item.value; // 수류탄 갯수
                    break;
                case Item.Type.Potion:
                    
                    break;
            }
            Destroy(other.gameObject);
        }
        else if(other.tag == "EnemyBullet")
        {
            if (!isdamage)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;

                bool isBossAtk = other.name == "Boss Melee Area";
                StartCoroutine(OnDamage(isBossAtk));
            }

            if (other.GetComponent<Rigidbody>() != null)
                Destroy(other.gameObject);
        }
    }

    void grenadeargorizm()
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

    

    IEnumerator OnDamage(bool isBossAtk)
    {
        isdamage = true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        }

        if (isBossAtk)
            rigid.AddForce(transform.forward * -25, ForceMode.Impulse);

        if (health <= 0 && !isDead)
            OnDie();

        yield return new WaitForSeconds(1f);

        isdamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }

        if (isBossAtk)
            rigid.velocity = Vector3.zero;

        
    }

    void childoff(GameObject obj)
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

    void Usepotion()
    {
        ParticleSystem buf = buffEffect.GetComponent<ParticleSystem>();
        var main = buf.main;

        if (havef1 && sdownf1 && !isbuff)
        {
            Potion(iteminfo1);
            havef1 = false;
            manager.isitembool1 = false;
            main.startColor = buffcolor;
        }

        if (havef2 && sdownf2 && !isbuff)
        {
            Potion(iteminfo2);
            havef2 = false;
            manager.isitembool2 = false;
            main.startColor = buffcolor;
        }

        if (havef3 && sdownf3 && !isbuff)
        {
            Potion(iteminfo3);
            havef3 = false;
            manager.isitembool3 = false;
            main.startColor = buffcolor;
        }
    }

    void Potion(int value)
    {
        isbuff = true;
        buffEffect.SetActive(true);
        GameObject hand = GameObject.Find("Weapon Point");
        Weapon Hammer = hand.transform.GetChild(0).gameObject.GetComponent<Weapon>();
        Weapon Handgun = hand.transform.GetChild(1).gameObject.GetComponent<Weapon>();
        Weapon Subgun = hand.transform.GetChild(2).gameObject.GetComponent<Weapon>();

        switch (value)
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


}
