using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemeyStats : MonoBehaviour
{
    private float health;   // Current Health. Líf púnktar
    public float maxHealth = 100; // MAX Health of the enemy. Max Líf
    [SerializeField] FloatingHealthBar healthBar;
    public float scoreValue = 10;     // Hversu mikið score leikmaður fær
    private GameManager gameManager;

    public GameObject[] powerUps; // Listi af power-ups
    public float dropChance = 0.75f; // Séns að gefa power-up

    

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Chekkar hvort collision hefur bullet tag
        if (collision.gameObject.CompareTag("bullet"))
        {
            pelletScript pellet = collision.gameObject.GetComponent<pelletScript>();
            if (pellet != null)
            {
                TakeDamage((float)pellet.pelletDamage); // Reiknar hvað óvinurinn missit mikið líf
                Destroy(collision.gameObject);
            }
        }
    }

    // Reiknar hvað óvinurinn missit mikið líf
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (healthBar != null)
        {
            Debug.Log(damage);
            healthBar.UpdateHealthBar(health, maxHealth);
        }

        if (health <= 0)
        {
            healthBar.gameObject.SetActive(false);
            Die(); // Eyðir sér þegar líf púnktar þess eru komnir í 0 
        }
    }

    // Höndlar dauða óvinsins
    void Die()
    {
        // Tween animation sem lætur óvininn skreppa saman og snúast í hringi
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(Vector3.zero, 0.5f)); // skreppur saman á 0.5 sek
        mySequence.Join(transform.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.LocalAxisAdd)); // snýr sér í hringi

        mySequence.OnComplete
        (() => 
        {
            Destroy(gameObject); // Eyðir sér þegar tween er búið
            gameManager.AddScore(scoreValue);
            AttemptDropPowerUp();
        }
        );
    }

    void AttemptDropPowerUp()
    {
        if (Random.value < dropChance) // Random.value gefur float á milli 0.0 and 1.0
        {
            int powerUpIndex = Random.Range(0, powerUps.Length); // Randomly velur power-up
            Instantiate(powerUps[powerUpIndex], transform.position, Quaternion.identity); // Birtir power up
        }
    }
}
