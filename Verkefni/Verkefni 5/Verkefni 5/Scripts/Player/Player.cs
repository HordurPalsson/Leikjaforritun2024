using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Player : MonoBehaviour
{
    // BREYTUR

    // Hljóð
    public AudioSource audioSource;
    public AudioClip shootClip;
    public AudioClip damageClip;

    // Movement
    public float moveSpeed = 5f; // Hraði
    private Rigidbody2D rb;


    // Animation
    public Transform sprite;
    public float waddleAmount = 10f;
    public float waddleDuration = 0.2f;


    // Pellet
    public GameObject pelletPrefab;
    public Transform firePoint;
    public float pelletSpeed = 3f;
    public float pelletDamage = 10;
    public float fireRate = 0.5f;
    private float fireDelay = 0f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        Movement();

        if (Input.GetButton("Fire1") && Time.time >= fireDelay)
        {
            Shoot();
            fireDelay = Time.time + 1f / fireRate;
        }
    }

    public void BoostStats()
    {
        fireRate += 0.15f;
        pelletSpeed += 0.25f;
        Debug.Log("Stats boosted");
    }

    private void Movement()
    {
        // Tekur input
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Býr til movement vector
        Vector3 movement = new Vector3(moveX, moveY, 0f);

        // Normalize
        movement *= moveSpeed * Time.deltaTime;

        // Hreyfir leikmanninn
        transform.Translate(movement);

        if (movement.magnitude > 0)
        {
            if (!DOTween.IsTweening(sprite))
            {
                StartWaddle();
            }
        }
        else
        {
            // Stoppar tween ef leikmaðurinn er ekki að hreyfa sig
            DOTween.Kill(sprite);
            sprite.rotation = Quaternion.identity; // Endurræsir rotation
        }
    }

    private void StartWaddle()
    {
        // Lætur leikmaninn vagga
        sprite.DORotate(new Vector3(0, 0, waddleAmount), waddleDuration)
              .SetEase(Ease.InOutSine)
              .SetLoops(-1, LoopType.Yoyo);
    }

    // Sér um að skjóta þar sem músin er
    private void Shoot()
    {
        audioSource.PlayOneShot(shootClip);
        // Instantiate
        GameObject pellet = Instantiate(pelletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
        pelletScript pelletScript = pellet.GetComponent<pelletScript>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; 

        Vector2 shootingDirection = (mousePos - firePoint.position).normalized;


        rb.velocity = shootingDirection * pelletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.DecreaseScore(10);
            Debug.Log("Score Decrease");
            audioSource.PlayOneShot(damageClip);
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            gameManager.DecreaseScore(30);
            Debug.Log("Score Decrease by pellet");
            audioSource.PlayOneShot(damageClip);
        }
    }
}
