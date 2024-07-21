using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public bool canFollow;
    private Transform target;

    public float smoothSpeed = 0.2f;
    public Vector3 offset;

    GameObject targetObj;

    void Start()
    {
        targetObj = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - targetObj.transform.position;
        offset.x = 0;
        target = targetObj.transform;
    }

    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target.position);
    }
}
