using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x ,0.76f, transform.position.z); //해당 포지션으로 이동
        transform.rotation = Quaternion.identity; // 로테이션 초기화
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