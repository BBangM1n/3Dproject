using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : Bullet
{
    public Transform target;
    NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position); // 타겟을 따라가기 위한 함수
    }
}
