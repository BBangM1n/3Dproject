using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target; // Ÿ�� ����
    public float speed; // ȸ�� �ӵ�
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;

        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime); // RotateAround ȸ�������ִ��Լ�

        offset = transform.position - target.position;
    }
}
