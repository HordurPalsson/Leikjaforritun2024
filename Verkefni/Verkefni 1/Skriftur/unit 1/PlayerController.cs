using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Breytur
    private float speed = 20.0f;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;


    // Keyrir áður en update byrjar
    void Start()
    {
        
    }

    // Uppfærir sig einu sinni hvert frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");


        // Hreyfir bílinn
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Stýrir bílnum hægri eða vinstri
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }
}
