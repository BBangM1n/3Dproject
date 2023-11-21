using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    public GameObject missile; // ���� �̻���
    public Transform missilePortA; // ���� �̻��� �Ա�
    public Transform missilePortB; // ���� �̻��� �Ա�

    Vector3 lookVec; //
    Vector3 tauntVec;

    public bool isLook;

    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        nav.isStopped = true;
        StartCoroutine(Think()); // ������ ���� �ڷ�ƾ ����
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) // ���� �� ��� �ڷ�ƾ ��ž
        {
            StopAllCoroutines();
            return;
        }

        if (isLook) // ������ �÷��̾ �Ĵٺ��� ����� �Լ�
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(Target.position + lookVec);
        }
        else
        {
            nav.SetDestination(tauntVec);
        }
    }

    IEnumerator Think() // ���ϻ���� ���� �ڷ�ƾ
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
                StartCoroutine(MissileShot());
                break;
            case 2:
            case 3:
                StartCoroutine(RockShot());
                break;
            case 4:
                StartCoroutine(TauntShot());
                break;

        }
    }

    IEnumerator MissileShot() // �̻��� ��
    {
        anim.SetTrigger("Shot");
        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();
        bossMissileA.target = Target; // �̻����� �÷��̾ ���󰡰� ����
        

        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        BossMissile bossMissileB = instantMissileB.GetComponent<BossMissile>();
        bossMissileB.target = Target;

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(Think()); // �ʱ�ȭ
    }

    IEnumerator RockShot() // �� ������ ����
    {
        isLook = false;
        anim.SetTrigger("BigShot");
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(3f);

        isLook = true;
        StartCoroutine(Think());
    }

    IEnumerator TauntShot() // ������ ����
    {
        tauntVec = Target.position + lookVec; // Ÿ�ٿ��� ���� ���� ���Ͱ�
        
        isLook = false;
        nav.isStopped = false;
        boxCollider.enabled = false;
        anim.SetTrigger("Taunt");
        yield return new WaitForSeconds(1.5f); // �����ؼ� ���� �ö� ���ݹ��� Ȱ��ȭ
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(3f); // �ʱ�ȭ

        isLook = true;
        nav.isStopped = true;
        boxCollider.enabled = true;
        StartCoroutine(Think());
    }
}
