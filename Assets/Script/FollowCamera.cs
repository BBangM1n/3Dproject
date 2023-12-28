using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public Transform BossPosition;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;
    public bool Cameraon = true;

    public bool isbosscoming;
    bool end = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Cameraon)
            transform.position = target.position + offset; // Å¸°ÙÀ» ÂÑ¾Æ°¡´Â Ä«¸Þ¶ó


        if (Input.GetKeyDown(KeyCode.G))
        {
            Cameraon = false;
            isbosscoming = true;
        }
        if(isbosscoming)
        {
            Vector3 Bosstarget = new Vector3(260, 30, 180);
            if (end == false)
                transform.position = Vector3.SmoothDamp(transform.position, Bosstarget, ref velocity, smoothTime);
            else
                transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);

            if (Vector3.Distance(Bosstarget, Camera.main.transform.position) < 1f)
            {
                end = true;
            }

            Debug.Log(Vector3.Distance(target.position + offset, Camera.main.transform.position));

            if (Vector3.Distance(target.position + offset, Camera.main.transform.position) < 1f && end == true)
            {
                Cameraon = true;
                isbosscoming = false;
                end = false;
            }
        }
    }
}
