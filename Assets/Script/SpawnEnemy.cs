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
        if(Enemycount <= 3 && !isspawn && !isboss) // ���� ���� ����
        {
            isspawn = true;
            StartCoroutine(Spawnenemy());
        }

        if(isboss && iscondition)
        {
            //���� �������� ������ �ö���� ��ȯ�Ϸ�Ǹ� �����ı��ǰ� ���� ������ �´�.
            GameObject instantEnemy = Instantiate(enemys[0], transform.position, transform.rotation);
        }
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
