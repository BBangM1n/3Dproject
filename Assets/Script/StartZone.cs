using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZone : MonoBehaviour
{
    public GameManager manager;
    public Vector3 spawnVt;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(manager.isBossbattle == false)
                other.gameObject.transform.position = spawnVt;
        }
    }
}
