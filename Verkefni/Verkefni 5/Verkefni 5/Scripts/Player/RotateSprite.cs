using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public Transform player; // staðsetning leikmanns
    public Camera mainCamera;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
    }


    // Function sem lætur sprite snúa sér að staðsetningu músarinnar
    private void RotateTowardsMouse()
    {
        // Sækir staðsetningu músarinnar
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Reiknar áttina frá leikmanninum að músini
        Vector3 direction = mousePosition - player.position;
        direction.z = 0; 

        // Reiknar út hvaða gráðu sprite-ið á að snúa sér að
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the sprite
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
