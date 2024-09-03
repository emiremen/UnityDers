using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 playerKameraUzakligi;
    [HideInInspector] public Vector3 kameraUzakligi;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        kameraUzakligi = playerKameraUzakligi;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + kameraUzakligi, smoothSpeed);

            //transform.position = Vector3.SmoothDamp(transform.position, target.position + kameraUzakligi, ref velocity, smoothSpeed);

            //transform.position = Vector3.MoveTowards(transform.position, target.position + kameraUzakligi, smoothSpeed);
        }
    }
}
