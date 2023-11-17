using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : Bullet
{
    public Transform target;
    NavMeshAgent nav;
    GameManager gmr;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        gmr = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);

        if (gmr.isBattle == false)
        {
            Destroy(gameObject);
        }
        else
            return;
    }
}
