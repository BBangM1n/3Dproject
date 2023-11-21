using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    public GameObject missile; // 보스 미사일
    public Transform missilePortA; // 보스 미사일 입구
    public Transform missilePortB; // 보스 미사일 입구

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
        StartCoroutine(Think()); // 패턴을 위한 코루틴 시작
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) // 죽을 시 모든 코루틴 스탑
        {
            StopAllCoroutines();
            return;
        }

        if (isLook) // 보스가 플레이어를 쳐다보게 만드는 함수
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

    IEnumerator Think() // 패턴사용을 위한 코루틴
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

    IEnumerator MissileShot() // 미사일 샷
    {
        anim.SetTrigger("Shot");
        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();
        bossMissileA.target = Target; // 미사일이 플레이어를 따라가게 유도
        

        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        BossMissile bossMissileB = instantMissileB.GetComponent<BossMissile>();
        bossMissileB.target = Target;

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(Think()); // 초기화
    }

    IEnumerator RockShot() // 돌 굴리기 패턴
    {
        isLook = false;
        anim.SetTrigger("BigShot");
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(3f);

        isLook = true;
        StartCoroutine(Think());
    }

    IEnumerator TauntShot() // 찍어누르기 패턴
    {
        tauntVec = Target.position + lookVec; // 타겟에게 가기 위한 벡터값
        
        isLook = false;
        nav.isStopped = false;
        boxCollider.enabled = false;
        anim.SetTrigger("Taunt");
        yield return new WaitForSeconds(1.5f); // 점프해서 내려 올때 공격범위 활성화
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(3f); // 초기화

        isLook = true;
        nav.isStopped = true;
        boxCollider.enabled = true;
        StartCoroutine(Think());
    }
}
