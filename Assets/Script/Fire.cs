using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x ,0.76f, transform.position.z); //�ش� ���������� �̵�
        transform.rotation = Quaternion.identity; // �����̼� �ʱ�ȭ
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy")) 
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.isfire = true;
        }
    }
}