using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform target; // Target
    public float smoothSpeed = 0.125f; // Hraði myndavélarinnar
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;


    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);


        transform.position = smoothedPosition;
    }
}
