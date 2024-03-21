using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropellerX : MonoBehaviour
{
    private float propellerSpeed = 850.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Lætur hreyfilinn snúast
        transform.Rotate(Vector3.forward * Time.deltaTime * propellerSpeed);
    }
}
