using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �κ��丮 ����
    int equipWeaponIndex = -1;
    int iteminfo1 = -1;
    int iteminfo2 = -1;
    int iteminfo3 = -1;

    // �̵����� �� ���� ������
    float haxis;
    float vaxis;
    float fireDelay;

    // �÷��̾� ����
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

    // ���� ������
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


    // ����Ʈ ���� �� ����
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
    void Update() // ������Ʈ���� ������ �ʵ��� �Լ� ��ɺ��� ����
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

    void GetInput() // Ű �Է� �Լ���
    {
        haxis = Input.GetAxisRaw("Horizontal"); // GetAxis -> Axis���� ������ ��ȯ
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
        moveVec = new Vector3(haxis, 0, vaxis).normalized; // normalized : ���� ���� 1�� ������ ����

        if (isdodge) // ������ ���� ��
            moveVec = dodgeVec;

        if (isswap || !isFireReady || isreload || isDead || isstop) // �� �����̴� ����
            moveVec = Vector3.zero;

        if(!isborder)
            transform.position += moveVec * speed * (wdown ? 0.3f : 1f) * Time.deltaTime;


        anim.SetBool("Run", moveVec != Vector3.zero);
        anim.SetBool("Walk", wdown);


        if (!isdodge && speed != 15 && !isbuff) // ���� �̵� �ӵ� ����
            speed = 15;
    }

    void Turn() // ĳ������ ����
    {
        // Ű���� ȸ�� 
        transform.LookAt(transform.position + moveVec); // LookAt : ������ ���͸� ���� ȸ�������ִ� �Լ�

        // ���콺 ȸ��
        if (fdown && !isdodge && !isDead)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);  // ScreenPointToRay ��ũ������ ����� Ray��� �Լ�
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100)) // out : returnó�� ��ȯ���� �־��� ������ �����ϴ� Ű����
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }

    void Dodge() // ������
    {
        if (jdown && moveVec != Vector3.zero && !isjump && !isdodge && !isswap && !isDead)
        {
            dodgeVec = moveVec;
            speed *= 2; // �������� �̵��ӵ�
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

    void Swap() // ���� ����
    {
        if(sdown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) // 1���� ������ �� 
        {
            return;
        }
        if (sdown2 && (!hasWeapons[1] || equipWeaponIndex == 1)) // 2���� ������ ��
        {
            return;
        }
        if (sdown3 && (!hasWeapons[2] || equipWeaponIndex == 2)) // 3���� ������ ��
        {
            return;
        }

        int weaponIndex = -1; // �ε��� �⺻��

        if (sdown1)
            weaponIndex = 0;
        if (sdown2)
            weaponIndex = 1;
        if (sdown3)
            weaponIndex = 2;

        if ((sdown1 || sdown2 || sdown3) && !isjump && !isdodge && !isDead) // ���� ���� �Լ�
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>(); // ���� �ε����� ���� ���� ��Ƽ�� Ȱ��ȭ
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

    void Interation() // ��ȣ�ۿ�Ű �Է�
    {
        if(idown && nearObject != null && !isjump && !isdodge && !isDead) // E ( ��ȣ�ۿ� ) ��ư�� ������ ��� �����մϴ�.
        {
            if(nearObject.tag == "Weapon") // ������ ���
            {
                Item item = nearObject.GetComponent<Item>();
                int weponIndex = item.value; // ������ �������� ���� �ε��� ����
                hasWeapons[weponIndex] = true; // �������� true �� ��ȯ

                // ������ ����
                if (weponIndex == 0)
                    DataManager.instance.nowPlayer.Weapon1 = true;
                else if (weponIndex == 1)
                    DataManager.instance.nowPlayer.Weapon2 = true;
                else if (weponIndex == 2)
                    DataManager.instance.nowPlayer.Weapon3 = true;
                Destroy(nearObject); // �ʵ忡 �ִ� ������ ����
            }
            else if (nearObject.tag == "Shop") // ������ ���
            {
                Shop shop = nearObject.GetComponent<Shop>(); // ���� ����
                shop.Enter(this);
                isshop = true; // isshop ���� Ȱ��ȭ
            }
            else if (nearObject.tag == "Potion") // ������ ���
            {
                // ���� F1, F2, F3ĭ ������ ���� ���� Ȯ�� �� �߰�
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
            Debug.Log("����ƮŬ�����ظ�");
        }
    }

    void Attack() // ����
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime; // ���� ������
        isFireReady = equipWeapon.rate < fireDelay;

        if(fdown && isFireReady && !isdodge && !isstop &&!isswap && !isshop && !isDead) // ���� Ű�� ������
        {
            equipWeapon.Use(); // ����ִ� ������ Use()�Լ� ���
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "Swing" : "Shot");
            fireDelay = 0;
        }
    }

    void Reload() // ������
    {
        if (equipWeapon == null) // �������� ���� ��� ����
            return;

        if (equipWeapon.type == Weapon.Type.Melee) // ����� ��� ����
            return;

        if (ammo == 0) // �Ѿ��� ���� ��� ����
            return;

        if(rdown && !isjump && !isswap && !isdodge && isFireReady && !isDead) // ������ ��ư�� ������
        {
            anim.SetTrigger("Reload");
            isreload = true;
            SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[5];
            SoundManager.instance.Effect_Sound.Play();
            Invoke("ReloadOut", 3f);

        }
    }

    void ReloadOut() // ������ ���� ��
    {
        // �ش� �Ѿ� ����
        int reAmmo = ammo < equipWeapon.maxammo ? ammo : equipWeapon.maxammo;
        equipWeapon.curammo = reAmmo;
        ammo -= reAmmo;
        DataManager.instance.nowPlayer.Ammo -= reAmmo;
        isreload = false;
    }

    void StopToWall() // �� �ձ� ����
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isborder = Physics.Raycast(transform.position, transform.forward , 5, LayerMask.GetMask("Wall"));
    }
    
    void Grenade() // ����ź
    {
        if (hasGrenade == 0) // ����ź�� �������� ���� ��
            return;

        if(gdown && !isreload && !isswap && !isDead) // ��Ŭ���� ������
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);  // ScreenPointToRay ��ũ������ ����� Ray��� �Լ� ����ź ���� ��ġ
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100)) // out : returnó�� ��ȯ���� �־��� ������ �����ϴ� Ű����
            {
                Vector3 nextVec = rayHit.point - transform.position; // ����ź�� �������� �������� ���� ���
                nextVec.y = 10;

                // ����
                GameObject instantGrenade = Instantiate(grenadeObj[GrenadeList[0]], transform.position, transform.rotation); // GrenadeList���ִ� ����ź�� �����մϴ�.
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>(); // �� ����ź�� ������Ʈ�� �����´�.
                rigidGrenade.AddForce(nextVec, ForceMode.Impulse); // ����ź�� �������鼭�� ���������� ����
                rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);
                GrenadeList.RemoveAt(0); // ����Ʈ ����
                hasGrenade--; // ����ź ���� �Ҹ�
                Qgrenade--; // ����Ʈ�� �������� �� ���� �Ҹ�
                childoff(grenades[hasGrenade]); // ȸ���ϴ� ����ź�� ��Ƽ�� ����

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
        if(other.tag == "Item") // �����ۿ� ������
        {
            Item item = other.GetComponent<Item>();
            switch (item.type) // ������ Ÿ���� �����մϴ�
            {
                case Item.Type.Ammo: // �Ѿ��̸� �Ѿ�����
                    ammo += item.value;
                    DataManager.instance.nowPlayer.Ammo += item.value;
                    if (ammo > maxammo)
                        ammo = maxammo;
                    break;
                case Item.Type.Coin: // �����̸� ��� ����
                    if(iscbuff) // ���� �����̸� �ι�
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
                case Item.Type.Heart: // ��Ʈ�� ���� HP ����
                    health += item.value;
                    if (health > maxhealth)
                        health = maxhealth;
                    break;
                case Item.Type.Grenade: // ����ź�̸� ����ź ����
                    if (hasGrenade == maxhasGrenade)
                        return;
                    //����ź ���� �Ǻ� �� �ֱ�
                    GrenadeList.Add(item.Grenadevalue);
                    //��ġ grenades�� 0, 1, 2, 3 �� 0�� ������ item.value������ ���ϵ� Ű��
                    grenades[hasGrenade].SetActive(true);
                    grenadeargorizm();
                    grenades[0].transform.GetChild(item.Grenadevalue).gameObject.SetActive(true);
                    hasGrenade += item.value; // ����ź ����
                    break;
            }
            Destroy(other.gameObject);
        }
        else if(other.tag == "EnemyBullet") // ����� �����̸�
        {
            if (!isdamage) // �������°� �ƴ� ��
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();

                if(defens < enemyBullet.damage) // ����� �������� �� ���� ���
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

    void grenadeargorizm() // ����ź �˰���
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

    

    IEnumerator OnDamage(bool isBossAtk) // �������� �Դ� ���
    {
        SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[4];
        SoundManager.instance.Effect_Sound.Play();

        isdamage = true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow; // �ǰݽ� ����� ��ȭ
        }

        if (isBossAtk)
            rigid.AddForce(transform.forward * -25, ForceMode.Impulse); // �������� ���� �� �з����� ����

        if (health <= 0 && !isDead)
            OnDie();

        yield return new WaitForSeconds(1f);

        isdamage = false;

        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white; // �ٽ� ���� �⺻��
        }

        if (isBossAtk)
            rigid.velocity = Vector3.zero; 
    }

    void childoff(GameObject obj) // �� �ֺ� ����ź ��Ƽ�� ����
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

    void Usepotion() // ���� ����
    {
        ParticleSystem buf = buffEffect.GetComponent<ParticleSystem>();
        var main = buf.main;
        // � Ű�� �������� �˾Ƴ���
        if (havef1 && sdownf1 && !isbuff) // F1�� ���
        {
            Potion(iteminfo1); // F1�� ��� ������ �ɷ��� �÷��̾�� �ο���ŵ�ϴ�
            havef1 = false; // �����Ǻ��� False�� �ٲߴϴ�
            manager.isitembool1 = false; // �����ϰ� ���� �ʰ� �մϴ�.
            main.startColor = buffcolor; // ���� ���� �°� Ȱ��ȭ�մϴ�.
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

    void Potion(int value) // ���� �ɷ� �ο� �Լ�
    {
        isbuff = true; // ���� ��ø ���� ������ ����
        buffEffect.SetActive(true); // ���� ����Ʈ�� ���ݴϴ�.
        GameObject hand = GameObject.Find("Weapon Point");
        Weapon Hammer = hand.transform.GetChild(0).gameObject.GetComponent<Weapon>();
        Weapon Handgun = hand.transform.GetChild(1).gameObject.GetComponent<Weapon>();
        Weapon Subgun = hand.transform.GetChild(2).gameObject.GetComponent<Weapon>();

        switch (value) // �� �ɷ��� �ο��� �ֱ����� switch�� �Ǻ����ݴϴ�.
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
