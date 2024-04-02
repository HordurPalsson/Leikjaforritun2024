using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Breytur
    public float speed = 155.0f;
    public float jumpForce = 7f;
    public float rotationSpeed = 10;
    public GameManager gameManager;
    public bool isOnGround = true;
    public bool gameOver;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {
            // Hreyfir leikmaninn samkvæmt innslátti frá leikmanni
            Movement();

            // Lætur leikmanninn hoppa
            JumpHandler();
        }
        
    }

    
    void Movement()
    {
        // Set x and z velocities to zero
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        // Distance ( speed = distance / time --> distance = speed * time)
        float distance = speed * Time.deltaTime;
        // Input on x ("Horizontal")
        float hAxis = Input.GetAxis("Horizontal");
        // Input on z ("Vertical")
        float vAxis = Input.GetAxis("Vertical");

        // Horizontal hreyfing
        float rotation = hAxis * rotationSpeed * Time.deltaTime;
        // Snýr leikmanninum
        transform.Rotate(0f, rotation, 0f);

        // Vertical hreyfing
        Vector3 movement = transform.forward * vAxis * distance;

        // Núverandi staðsetning
        Vector3 currPosition = transform.position;
        // nýja staðsetningin
        Vector3 newPosition = currPosition + movement;

        // Færir rigid body
        rb.MovePosition(newPosition);
    }

    
    void JumpHandler()
    {
        // Jump axis
        float jAxis = Input.GetAxis("Jump");
        if (jAxis > 0f && isOnGround)
        {
            isOnGround = false;
            // Jumping vector
            Vector3 jumpVector = new Vector3(0f, jumpForce, 0f);
            // Lætur leikmannin stökkva
            rb.velocity = rb.velocity + jumpVector;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Kannar hvort við leikmaðurinn er í loftinu eða ekki
        if (collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
        }

    }
    
    void OnTriggerEnter(Collider collider)
    {
        // Kíkir á hvort við snertum krónu
        if (collider.gameObject.tag == "Coin")
        {
            gameManager.CoinsCollected(1);
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.tag == "Enemy")
        {
            // Destroy
            Destroy(collider.gameObject);
        }
    }

}
