using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target; // Ÿ�� ����
    public float speed; // ȸ�� �ӵ�
    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }


    void Update()
    {
        transform.position = target.position + offset;

        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime); // RotateAround ȸ�������ִ��Լ�

        offset = transform.position - target.position;
    }
}
