using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Breytur
    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -7);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Eltir Bílinn með smá fjarlægð á milli
        transform.position = player.transform.position + offset;
    }
}
