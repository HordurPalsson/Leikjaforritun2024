using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    public float floatSpeed = 1f;   // Hraðinn
    public float floatHeight = 0.5f; // Hversu hátt hluturinn fer upp og niður
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Reiknar út hvert hluturinn hreyfir sig
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Færir hlutinn
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
