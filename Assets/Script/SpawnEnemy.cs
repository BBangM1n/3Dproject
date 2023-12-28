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
        if(Enemycount <= 3 && !isspawn && !isboss) // ���� ���� ����
        {
            isspawn = true;
            StartCoroutine(Spawnenemy());
        }

        if(isboss)
        {
            if(manager.BossCounting > 2)
            {
                StartCoroutine(BossSpawn());
                //GameObject instantEnemy = Instantiate(enemys[0], transform.position, transform.rotation);
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
        StopCoroutine(thisCoroutine);
        yield return new WaitForSeconds(1f);
        FollowCamera camera = GameObject.Find("Main Camera").gameObject.GetComponent<FollowCamera>();
        camera.Cameraon = false;
        camera.isbosscoming = true;

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
