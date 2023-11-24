using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ice : MonoBehaviour // ���̽� ����ź �ߵ�
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0.76f, transform.position.z);
        transform.rotation = Quaternion.identity;
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") // ��� enemy�鿡�� ����
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(!enemy.isice)
            {
                enemy.nav.speed /= 2; // �̵��ӵ� ������
                enemy.isice = true;
 
            }
        }
    } 
}
