using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brute : MonoBehaviour
{
    public Transform player;        // Staðsetning leikmanns
    public float speed = 5.0f;      // Hraði óvinsins
    public float dashSpeed = 10.0f;    // Hraði sprettsins
    public float dashDistance = 2.0f;  // Fjarlægðin sem enemy þarf að vera í til þess að spretta
    public float dashCooldown = 4.0f;  // Cooldown fyrir sprett

    private float lastDashTime = 0;    // Síðasta skiptið sem var sprett
    private bool isDashing = false;    // Bool sem fylgist með hvort er verið að spretta
    

    public void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }


    void Update()
    {
        if (isDashing)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= dashDistance && Time.time > lastDashTime + dashCooldown)
        {
            StartCoroutine(DashTowardsPlayer(distance));
        }
        else
        {
            ChasePlayer();
        }
    }

    // Lætur Brute spretta að leikmanninum þegar hann kemst nógu nálægt
    IEnumerator DashTowardsPlayer(float distance)
    {
        isDashing = true;
        lastDashTime = Time.time;

        // Sprettir
        Vector3 startPosition = transform.position;
        Vector3 endPosition = player.position;
        
        float dashTime = distance / dashSpeed;
        float elapsedTime = 0;

        while (elapsedTime < dashTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / dashTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    // Lætur Brute elta leikmanninn
    void ChasePlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Move the enemy towards the player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
