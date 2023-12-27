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
    public bool isboss;
    public bool iscondition;

    Player player;
    GameManager manager;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemycount <= 3 && !isspawn && !isboss) // 일정 갯수 제한
        {
            isspawn = true;
            StartCoroutine(Spawnenemy());
        }

        if(isboss && iscondition)
        {
            //보스 시점연출 보스가 올라오고 소환완료되면 지형파괴되고 이제 나에게 온다.
            GameObject instantEnemy = Instantiate(enemys[0], transform.position, transform.rotation);
        }
    }

    IEnumerator Spawnenemy() // 몬스터 생성 코루틴
    {
        yield return new WaitForSeconds(10f);
        int i = Random.Range(0, enemys.Length);
        GameObject instantEnemy = Instantiate(enemys[i], transform.position, transform.rotation);
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
