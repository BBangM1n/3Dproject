using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // 몹이 일정 수량 만큼 스폰되게 할거임
    // 물론 겹칠 수 있음
    // 단, 마릿수 제한 만큼만 나오기 
    public GameObject[] enemys; // 몬스터 저장
    public int Enemycount; // 몬스터 갯수
    bool isspawn; // 스폰중이면 못나오게

    [Header("--- 보스 ---")]
    public bool isboss;
    public bool bosson;
    public GameObject TreeAndRock;

    Player player;
    GameManager manager;
    public Coroutine thisCoroutine;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(Enemycount <= 2 && !isspawn && !isboss && !manager.isBossbattle) // 스폰되는 몬스터 일정 갯수 제한 총 3마리
        {
            isspawn = true;
            StartCoroutine(Spawnenemy());
        }
        else
        {
            StopCoroutine(Spawnenemy());
        }

        if(isboss)
        {
            if(manager.BossCounting > 4 ) // 보스는 5마리를 잡으면 소환되게 설정
            {
                manager.isBossbattle = true;
                StartCoroutine(BossSpawn());
                SoundManager.instance.SoundChange(2); // BGM 변경
                SoundManager.instance.isboss = true;
            }

        }
    }
    IEnumerator BossSpawn() // 보스가 소환될때
    {
        manager.BossCounting = 0; // 보스가 다시 소환 될 수 있도록 카운팅 0으로
        manager.BossComing.SetActive(true); // 보스 출현 임펙트
        thisCoroutine = StartCoroutine(manager.BossCreateText()); // 보스출현 텍스트 발동

        yield return new WaitForSeconds(4f);

        manager.BossComing.SetActive(false);
        StopCoroutine(thisCoroutine); // 보스출현 텍스트 중지

        yield return new WaitForSeconds(1f);

        player.isstop = true; // 플레이어 정지

        // 카메라 연출
        FollowCamera camera = GameObject.Find("Main Camera").gameObject.GetComponent<FollowCamera>();
        camera.Cameraon = false;
        camera.isbosscoming = true;
        Vector3 BossVt = transform.position;
        BossVt = new Vector3(BossVt.x, BossVt.y -20 , BossVt.z);
        GameObject instantEnemy = Instantiate(enemys[0], BossVt, transform.rotation); // 보스 소환

        yield return new WaitForSeconds(8f);

        TreeAndRock.SetActive(false);

        yield return new WaitForSeconds(1f);

        camera.end = true;

        yield return new WaitForSeconds(10f);

        player.isstop = false;
        // 보스 설정
        Boss enemy = instantEnemy.GetComponent<Boss>();
        enemy.Target = player.transform;
        enemy.manager = manager;
        manager.boss = enemy;
        StartCoroutine(enemy.Think());
        enemy.notspawn = true;
    }

    IEnumerator Spawnenemy() // 몬스터 생성 코루틴
    {
        yield return new WaitForSeconds(10f);
        int i = Random.Range(0, enemys.Length);
        GameObject instantEnemy = Instantiate(enemys[i], transform.position, transform.rotation);
        // 소환한 몬스터 기본설정
        Enemy enemy = instantEnemy.GetComponent<Enemy>();
        enemy.Spawnposition = gameObject.transform;
        enemy.Target = player.transform;
        enemy.manager = manager;
        enemy.spawnenemy = this;
        Enemycount++;
        yield return new WaitForSeconds(5f);
        isspawn = false;
    }
}
