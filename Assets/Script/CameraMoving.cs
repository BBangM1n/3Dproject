using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform targetPosition;

    public float smoothTime;

    private Vector3 velocity = Vector3.zero;

    public bool isActive = false;


    private void Start()
    {

    }

    private void Update()
    {
        if (isActive)
        {
            Vector3 target = new Vector3(targetPosition.position.x, targetPosition.position.y + 20, targetPosition.position.z);
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, target, ref velocity, smoothTime);

            if (Vector3.Distance(targetPosition.position, Camera.main.transform.position) < 0.1f)
            {
                isActive = false;
            }
        }
    }
}
