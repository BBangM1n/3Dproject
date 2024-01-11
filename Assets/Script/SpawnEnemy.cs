using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // ���� ���� ���� ��ŭ �����ǰ� �Ұ���
    // ���� ��ĥ �� ����
    // ��, ������ ���� ��ŭ�� ������ 
    public GameObject[] enemys; // ���� ����
    public int Enemycount; // ���� ����
    bool isspawn; // �������̸� ��������

    [Header("--- ���� ---")]
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemycount <= 2 && !isspawn && !isboss && !manager.isBossbattle) // ���� ���� ����
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
            if(manager.BossCounting > 2)
            {
                StartCoroutine(BossSpawn());
                Debug.Log("�������´ٿ���");
            }

        }
    }
    IEnumerator BossSpawn()
    {
        manager.BossCounting = 0;
        manager.BossComing.SetActive(true);
        thisCoroutine = StartCoroutine(manager.BossCreateText());
        yield return new WaitForSeconds(4f);
        manager.BossComing.SetActive(false);
        manager.isBossbattle = true;
        StopCoroutine(thisCoroutine);
        yield return new WaitForSeconds(1f);
        player.isreload = true;
        FollowCamera camera = GameObject.Find("Main Camera").gameObject.GetComponent<FollowCamera>();
        camera.Cameraon = false;
        camera.isbosscoming = true;
        Vector3 BossVt = transform.position;
        BossVt = new Vector3(BossVt.x, BossVt.y -20 , BossVt.z);
        GameObject instantEnemy = Instantiate(enemys[0], BossVt, transform.rotation);
        yield return new WaitForSeconds(8f);
        TreeAndRock.SetActive(false);
        yield return new WaitForSeconds(1f);
        camera.end = true;
        yield return new WaitForSeconds(10f);
        player.isreload = false;
        Boss enemy = instantEnemy.GetComponent<Boss>();
        enemy.Target = player.transform;
        enemy.manager = manager;
        manager.boss = enemy;
        StartCoroutine(enemy.Think());
        enemy.notspawn = true;
        Debug.Log("�����ڷ�ƾ������");
    }

    IEnumerator Spawnenemy() // ���� ���� �ڷ�ƾ
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
