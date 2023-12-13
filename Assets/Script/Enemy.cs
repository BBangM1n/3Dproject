using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C, D }; // 몬스터 타입별 구분
    public Type enemyType;
    public int maxHealth; // 최대 체력
    public int curHealth; // 현재 체력
    public int score; // 점수
    public GameManager manager;
    public Transform Target;
    public Transform Spawnposition;
    public BoxCollider meleeArea;
    public GameObject bullet;
    public GameObject[] coins;

    // 현재 상태
    public bool isChase;
    public bool isAttack;
    public bool isDead;
    public bool isdamage;

    // 상태이상 여부
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
        if (nav.enabled && enemyType != Type.D) // 자동으로 플레이어 추적하기
        {   
            if(Vector3.Distance(transform.position, Spawnposition.position) < 70 && !isDead) // 몬스터와 스폰장소길이가 50보다 작을때 까지 플레이어를 추적
            {
                nav.SetDestination(Target.position); // SetDestination : 도착할 목표 위치 정할 함수
                nav.isStopped = !isChase;
            }
        }

        // 몬스터와 플레이어 길이가 50보다 길거나 플레이어가 몬스터스폰장소의 길이차이가 70일때
        if (Vector3.Distance(transform.position, Target.position) > 50 && !isDead || Vector3.Distance(Target.position, Spawnposition.position) > 70)
        {
            nav.enabled = false;
            if (Vector3.Distance(transform.position, Spawnposition.position) > 5f)
            {
                // 목표 방향을 바라보는 함수 호출
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

        // 이동을 부드럽게 하기 위해 SmoothDamp 함수 사용
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
        Targerting();
    }

    void Targerting() // 타겟이 반경안에 있으면 공격하도록 만드는 함수
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

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player")); // 타입별 원형 레이를 만들어 타입별만의 범위안에 들어오면 공격하도록 함

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack() // 공격기능 코루틴
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("Attack", true);

        switch (enemyType) // 타입별 공격 방식
        {
            case Type.A: // 초록 공룡 기본 박치기 공격
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;

            case Type.B: // 보라 공룡 돌진 공격
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse); // 달려가는 힘 증가
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);
                break;

            case Type.C: // 노랑 공룡 미사일 공격
                yield return new WaitForSeconds(0.5f);
                GameObject instatBullet = Instantiate(bullet, transform.position, transform.rotation); // 플레이어에게 미사일 생성 후 공격
                Rigidbody rigidBullet = instatBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 20f;

                yield return new WaitForSeconds(2f);
                break;
        }
        
        
        isChase = true;
        isAttack = false;
        anim.SetBool("Attack", false);
    }

    void ChaseStart() // 패턴이 끝나고 다시 기본 움직임
    {
        isChase = true;
        anim.SetBool("Walk",true);
    }

    void FreezeVelocity() // 갑작스러운 부딫힘으로 인한 회전력 zero값.
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee") // 플레이어의 망치에 당할 때 함수
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;

            if (!isDead)
                StartCoroutine(OnDamage(reactVec, false));
        }
        else if(other.tag == "Bullet") // 플레이어의 총에 당할 때 함수
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);
            if(!isDead) // 조건이 없을시 여러번의 죽음현상 발견
                StartCoroutine(OnDamage(reactVec, false));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec, bool isGrenade) // 데미지 입었을 시 호출되는 코루틴
    {
        foreach (MeshRenderer mesh in meshs) // 피격 시 몸 색깔 변경 (빨강)
            mesh.material.color = Color.red;
 
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0) // 피격 시 Hp여부에 따른 함수
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.white; // 다시 원래 색깔로 돌아간다.

        }
        else // 죽은 판정
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.gray; // 회색 색깔로 변경

            gameObject.layer = 10;
            isDead = true;
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("Die");
            Player player = Target.GetComponent<Player>();
            player.score += score;
            int ranCoin = Random.Range(0, 3);
            Instantiate(coins[ranCoin], transform.position, Quaternion.identity); // 코인 떨어트리는 기능
            player.Qenemy--;
            spawnenemy.Enemycount--;

            switch (enemyType) // 타입별 퀘스트전용 조건
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



            if (isGrenade) // 죽을 시 시체처리 (수류탄ver)
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

    void Debuff() // 수류탄에 의한 디버프 오오라 생성.
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

    public void HitByGrenade(Vector3 explosionPos) // 플레이어 수류탄에 맞을 때
    {
        curHealth -= 100;
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec, true));
    }

    IEnumerator fireon() // 화염 수류탄에 맞았을 때
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

    IEnumerator fireoff() // 화염 수류탄 끝났을 때
    {
        yield return new WaitForSeconds(10f);
        isfire = false;
        Debuffeffect[1].SetActive(false);
        StopCoroutine(fireoff());
    }

    IEnumerator Iceoff() // 얼음 수류탄 끝났을 때
    {
        isice = false;
        yield return new WaitForSeconds(10f);
        Debuffeffect[0].SetActive(false);
        nav.speed *= 2;
    }
}
