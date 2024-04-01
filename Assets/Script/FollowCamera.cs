using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 카메라 타겟
    public Vector3 offset; // 카메라 위치 조절을 위한 벡터값

    public Transform BossPosition; // 보스 연출을 위한 값
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;
    public bool Cameraon = true;

    public bool isbosscoming;
    public bool end = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Cameraon)
            transform.position = target.position + offset; // 타겟을 쫓아가는 카메라

        if(isbosscoming)
        {
            Vector3 Bosstarget = new Vector3(260, 30, 180);
            if (end == false)
                transform.position = Vector3.SmoothDamp(transform.position, Bosstarget, ref velocity, smoothTime);
            else
                transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);

            if (Vector3.Distance(target.position + offset, Camera.main.transform.position) < 2f && end == true)
            {
                Cameraon = true;
                isbosscoming = false;
                end = false;
            }
        }
    }
}
