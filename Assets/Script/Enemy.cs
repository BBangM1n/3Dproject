using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C, D }; // ���� Ÿ�Ժ� ����
    public Type enemyType;
    public int maxHealth; // �ִ� ü��
    public int curHealth; // ���� ü��
    public int score; // ����
    public GameManager manager;
    public Transform Target;
    public Transform Spawnposition;
    public BoxCollider meleeArea;
    public GameObject bullet;
    public GameObject[] coins;

    // ���� ����
    public bool isChase;
    public bool isAttack;
    public bool isDead;
    public bool isdamage;

    // �����̻� ����
    public bool isfire = false;
    public bool isice = false;
    public bool ispoison = false;

    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public MeshRenderer[] meshs;
    public NavMeshAgent nav;
    public Animator anim;

    public GameObject[] Debuffeffect;
    public SpawnEnemy spawnenemy;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        if(enemyType != Type.D)
        Invoke("ChaseStart", 2);

    }

    private void Update()
    {
        if (nav.enabled && enemyType != Type.D) // �ڵ����� �÷��̾� �����ϱ�
        {   
            if(Vector3.Distance(transform.position, Spawnposition.position) < 70 && !isDead) // ���Ϳ� ������ұ��̰� 50���� ������ ���� �÷��̾ ����
            {
                nav.SetDestination(Target.position); // SetDestination : ������ ��ǥ ��ġ ���� �Լ�
                nav.isStopped = !isChase;
            }
        }

        // ���Ϳ� �÷��̾� ���̰� 50���� ��ų� �÷��̾ ���ͽ�������� �������̰� 70�϶�
        if (Vector3.Distance(transform.position, Target.position) > 50 && !isDead || Vector3.Distance(Target.position, Spawnposition.position) > 70)
        {
            nav.enabled = false;
            if (Vector3.Distance(transform.position, Spawnposition.position) > 5f)
            {
                // ��ǥ ������ �ٶ󺸴� �Լ� ȣ��
                LookAtSmooth(Spawnposition.position, 0.1f);
            }
        }
        else
        {
            nav.enabled = true;
        }

        Debuff();
    }

    private void LookAtSmooth(Vector3 targetPosition, float smoothTime)
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, smoothTime);

        // �̵��� �ε巴�� �ϱ� ���� SmoothDamp �Լ� ���
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
        Targerting();
    }

    void Targerting() // Ÿ���� �ݰ�ȿ� ������ �����ϵ��� ����� �Լ�
    {
        if (!isDead && enemyType != Type.D)
        {
            float targetRadius = 0;
            float targetRange = 0;

            switch (enemyType)
            {
                case Type.A:
                    targetRadius = 1.5f;
                    targetRange = 3f;
                    break;

                case Type.B:
                    targetRadius = 1f;
                    targetRange = 12f;
                    break;

                case Type.C:
                    targetRadius = 0.5f;
                    targetRange = 25f;
                    break;
            }

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player")); // Ÿ�Ժ� ���� ���̸� ����� Ÿ�Ժ����� �����ȿ� ������ �����ϵ��� ��

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack() // ���ݱ�� �ڷ�ƾ
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("Attack", true);

        switch (enemyType) // Ÿ�Ժ� ���� ���
        {
            case Type.A: // �ʷ� ���� �⺻ ��ġ�� ����
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;

            case Type.B: // ���� ���� ���� ����
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse); // �޷����� �� ����
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);
                break;

            case Type.C: // ��� ���� �̻��� ����
                yield return new WaitForSeconds(0.5f);
                GameObject instatBullet = Instantiate(bullet, transform.position, transform.rotation); // �÷��̾�� �̻��� ���� �� ����
                Rigidbody rigidBullet = instatBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 20f;

                yield return new WaitForSeconds(2f);
                break;
        }
        
        
        isChase = true;
        isAttack = false;
        anim.SetBool("Attack", false);
    }

    void ChaseStart() // ������ ������ �ٽ� �⺻ ������
    {
        isChase = true;
        anim.SetBool("Walk",true);
    }

    void FreezeVelocity() // ���۽����� �΋H������ ���� ȸ���� zero��.
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee") // �÷��̾��� ��ġ�� ���� �� �Լ�
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;

            if (!isDead)
                StartCoroutine(OnDamage(reactVec, false));
        }
        else if(other.tag == "Bullet") // �÷��̾��� �ѿ� ���� �� �Լ�
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);
            if(!isDead) // ������ ������ �������� �������� �߰�
                StartCoroutine(OnDamage(reactVec, false));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec, bool isGrenade) // ������ �Ծ��� �� ȣ��Ǵ� �ڷ�ƾ
    {
        foreach (MeshRenderer mesh in meshs) // �ǰ� �� �� ���� ���� (����)
            mesh.material.color = Color.red;
 
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0) // �ǰ� �� Hp���ο� ���� �Լ�
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.white; // �ٽ� ���� ����� ���ư���.

        }
        else // ���� ����
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.gray; // ȸ�� ����� ����

            gameObject.layer = 10;
            isDead = true;
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("Die");
            Player player = Target.GetComponent<Player>();
            player.score += score;
            int ranCoin = Random.Range(0, 3);
            Instantiate(coins[ranCoin], transform.position, Quaternion.identity); // ���� ����Ʈ���� ���
            player.Qenemy--;
            spawnenemy.Enemycount--;

            switch (enemyType) // Ÿ�Ժ� ����Ʈ���� ����
            {
                case Type.A:
                    manager.enemyCntA--;
                    player.enemyAcount--;
                    break;
                case Type.B:
                    manager.enemyCntB--;
                    player.enemyBcount--;
                    break;
                case Type.C:
                    manager.enemyCntC--;
                    player.enemyCcount--;
                    break;
                case Type.D:
                    manager.enemyCntD--;
                    player.enemyDcount--;
                    break;
            }



            if (isGrenade) // ���� �� ��üó�� (����źver)
            {
                reactVec = reactVec.normalized;
                reactVec += Vector3.up * 3;

                rigid.freezeRotation = false;
                rigid.AddForce(reactVec * 5, ForceMode.Impulse);
                rigid.AddTorque(reactVec * 15, ForceMode.Impulse);
            }
            else
            {
                reactVec = reactVec.normalized;
                reactVec += Vector3.up;
                rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            }

            Destroy(gameObject, 4);
        }
    }

    void Debuff() // ����ź�� ���� ����� ������ ����.
    {
        if(isice)
        {
            StartCoroutine(Iceoff());
            Debuffeffect[0].SetActive(true);
        }

        if(isfire)
        {
            Debuffeffect[1].SetActive(true);
            if (!isdamage)
            {
                isdamage = true;
                StartCoroutine(fireon());
            }
            StartCoroutine(fireoff());
        }
    }

    public void HitByGrenade(Vector3 explosionPos) // �÷��̾� ����ź�� ���� ��
    {
        curHealth -= 100;
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec, true));
    }

    IEnumerator fireon() // ȭ�� ����ź�� �¾��� ��
    {
        if (curHealth <= 0)
            StopCoroutine(OnDamage(transform.position, false));
        else
        {
            curHealth -= 5;
            StartCoroutine(OnDamage(transform.position, false));
        }
        yield return new WaitForSeconds(1f);
        isdamage = false;
    }

    IEnumerator fireoff() // ȭ�� ����ź ������ ��
    {
        yield return new WaitForSeconds(10f);
        isfire = false;
        Debuffeffect[1].SetActive(false);
        StopCoroutine(fireoff());
    }

    IEnumerator Iceoff() // ���� ����ź ������ ��
    {
        isice = false;
        yield return new WaitForSeconds(10f);
        Debuffeffect[0].SetActive(false);
        nav.speed *= 2;
    }
}
