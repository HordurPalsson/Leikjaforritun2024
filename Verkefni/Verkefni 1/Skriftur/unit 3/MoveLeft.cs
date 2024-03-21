using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // Breytur
    private float speed = 30;
    private PlayerController playerControllerScript;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Stoppar hreyfingu ef leikmaðurinn fer utan í "Obstacle"
        if (playerControllerScript.gameOver == false)
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        
        // Eyðir "Obstacle" eftir að hann fer fyrir utan skjáinn
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle")) {
            Destroy(gameObject);
        } 
    }
}
