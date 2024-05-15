using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Camera mainCamera; // Reference to the main camera


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Calculate the direction from the player to the mouse
        Vector3 direction = mousePosition - player.position;
        direction.z = 0; // Ensure we only rotate on the Z axis

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the sprite
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
