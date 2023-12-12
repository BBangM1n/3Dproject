using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // ���� ���� ���� ��ŭ �����ǰ� �Ұ���
    // ���� ��ĥ �� ����
    // ��, ������ ���� ��ŭ�� ������ 
    public GameObject[] enemys; // ���� ����
    public int Enemycount; // ���� ���� ����
    bool isspawn; // �������̸� ��������

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
        if(Enemycount <= 3 & !isspawn) // ���� ���� ����
        {
            isspawn = true;
            StartCoroutine(Spawnenemy());
        }
    }

    IEnumerator Spawnenemy() // ���� ���� �ڷ�ƾ
    {
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
