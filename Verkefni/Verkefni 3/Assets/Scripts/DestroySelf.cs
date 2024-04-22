using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float delay = 3f;

    void Start()
    {
        Invoke("DestroyItem", delay);
    }

    public void DestroyItem()
    {

        Destroy(gameObject);
    }
}
