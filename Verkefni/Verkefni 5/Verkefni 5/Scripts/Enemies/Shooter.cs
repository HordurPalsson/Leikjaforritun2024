using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Breytur
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;  

    public float speed = 2.0f;
    public float stopDistance = 10.0f;
    public float tooCloseDistance = 5.0f;
    public bool isMovingAway = false;

    private float lastShotTime; 
    public float shootingInterval = 2.0f;
    public float pelletSpeed = 3f;
    

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        lastShotTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        AttemptToShoot();
    }

    // Eltir leikmanninn en heldur sammt smá fjarlægð
    void ChasePlayer()
    {
        if (player != null)
        {
            // Reiknar áttina að leikmanninum
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Reiknar fjarlægð frá leikmanni
            float distance = Vector3.Distance(transform.position, player.position);

            // Ræður hverning á að hreyfa sig
            if (distance < tooCloseDistance)
            {
                isMovingAway = true;
                // Fer í burtu frá leikmanni
                transform.position -= direction * speed * Time.deltaTime;
            }
            else if (distance > stopDistance)
            {
                isMovingAway = false;
                // Færir sig að leikmanni
                transform.position += direction * speed * Time.deltaTime;
            }
            else
            {
                // Hættir að hreyfa sig
                isMovingAway = false;
            }
        }
    }

    void AttemptToShoot()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            // Kýkir hvort það getur skotið
            if (distance <= stopDistance && distance >= tooCloseDistance && Time.time > lastShotTime + shootingInterval)
            {
                Shoot();
                lastShotTime = Time.time;  // Uppfærir hvenær síðast var skotið
            }
        }
    }

    // Sér um að skjóta pellets
    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        pelletScript pelletScript = projectile.GetComponent<pelletScript>();

        Vector2 direction = (player.position - firePoint.position).normalized;

        rb.velocity = direction * pelletSpeed;
    }
}
