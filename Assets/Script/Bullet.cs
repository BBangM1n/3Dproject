using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public bool isRock;

    private void OnCollisionEnter(Collision collision)
    {
        if(!isRock && collision.gameObject.tag == "Floor") // 바닥에 닿으면 사라지게
        {
            Destroy(gameObject, 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isMelee && other.gameObject.tag == "Wall") // 벽이면 사라지게
        {
            Destroy(gameObject);
        }
        else if(!isMelee && other.gameObject.tag == "Floor") // 바닥에 닿으면 사라지게
        {
            Destroy(gameObject, 3);
        }
    }
}
