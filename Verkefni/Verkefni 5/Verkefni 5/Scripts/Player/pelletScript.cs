using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelletScript : MonoBehaviour
{
    public float lifeSpan = 2f;
    public float pelletDamage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
