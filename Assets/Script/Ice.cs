using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ice : MonoBehaviour // 아이스 수류탄 발동
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
        if (other.gameObject.tag == "Enemy") // 닿는 enemy들에게 적용
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(!enemy.isice)
            {
                enemy.nav.speed /= 2; // 이동속도 반으로
                enemy.isice = true;
 
            }
        }
    } 
}
