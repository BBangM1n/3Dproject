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

    void Update()
    {
        if(Enemycount <= 2 && !isspawn && !isboss && !manager.isBossbattle) // �����Ǵ� ���� ���� ���� ���� �� 3����
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
            if(manager.BossCounting > 4 ) // ������ 5������ ������ ��ȯ�ǰ� ����
            {
                manager.isBossbattle = true;
                StartCoroutine(BossSpawn());
                SoundManager.instance.SoundChange(2); // BGM ����
                SoundManager.instance.isboss = true;
            }

        }
    }
    IEnumerator BossSpawn() // ������ ��ȯ�ɶ�
    {
        manager.BossCounting = 0; // ������ �ٽ� ��ȯ �� �� �ֵ��� ī���� 0����
        manager.BossComing.SetActive(true); // ���� ���� ����Ʈ
        thisCoroutine = StartCoroutine(manager.BossCreateText()); // �������� �ؽ�Ʈ �ߵ�

        yield return new WaitForSeconds(4f);

        manager.BossComing.SetActive(false);
        StopCoroutine(thisCoroutine); // �������� �ؽ�Ʈ ����

        yield return new WaitForSeconds(1f);

        player.isstop = true; // �÷��̾� ����

        // ī�޶� ����
        FollowCamera camera = GameObject.Find("Main Camera").gameObject.GetComponent<FollowCamera>();
        camera.Cameraon = false;
        camera.isbosscoming = true;
        Vector3 BossVt = transform.position;
        BossVt = new Vector3(BossVt.x, BossVt.y -20 , BossVt.z);
        GameObject instantEnemy = Instantiate(enemys[0], BossVt, transform.rotation); // ���� ��ȯ

        yield return new WaitForSeconds(8f);

        TreeAndRock.SetActive(false);

        yield return new WaitForSeconds(1f);

        camera.end = true;

        yield return new WaitForSeconds(10f);

        player.isstop = false;
        // ���� ����
        Boss enemy = instantEnemy.GetComponent<Boss>();
        enemy.Target = player.transform;
        enemy.manager = manager;
        manager.boss = enemy;
        StartCoroutine(enemy.Think());
        enemy.notspawn = true;
    }

    IEnumerator Spawnenemy() // ���� ���� �ڷ�ƾ
    {
        yield return new WaitForSeconds(10f);
        int i = Random.Range(0, enemys.Length);
        GameObject instantEnemy = Instantiate(enemys[i], transform.position, transform.rotation);
        // ��ȯ�� ���� �⺻����
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
