using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Breytur
    public GameObject Player;
    public Vector3 offset = new Vector3(0, 1, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        // Færir leikmaninn á byrjenda reitinn
        Player.transform.position = transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
