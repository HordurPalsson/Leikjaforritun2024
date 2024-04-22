using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // breytur
    public bool isDead;
    public float playerHealth;
    public float playerHealthMax = 100;
    public float chipSpeed = 2f;
    private float lerpTimer;
    public Image frontHealthBar;
    public Image backHealthBar;
    public AudioSource goodthing;
    public AudioSource badthing;
    public AudioSource shotSound;
    public Vector3 shootPointOffset;
    public GameObject projectilePrefab;
    public float shootForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // passar að leikmaðurinn sé ekki dauður þegar leikurinn byrjar
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        // passar að health fer aldrei lægra en 0 eða hærra en 100
        playerHealth = Mathf.Clamp(playerHealth, 0, playerHealthMax);
        UpdateHealthUI();
        if (playerHealth <= 0)
        {
            Death();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isDead)
        {
            Shoot();
        }

    }

    void OnCollisionEnter(Collision other)
    {
        // Ef leikmaðurinn snertir vatn deyr hann
        if (other.gameObject.CompareTag("Water"))
        {
            Death();
        }
    }

    void Shoot()
    {
        // Calculate the shoot point position based on the player's position and offset
        Vector3 shootPosition = transform.position + shootPointOffset;

        GameObject projectile = Instantiate(projectilePrefab, shootPosition, transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            shotSound.Play();
            projectileRb.AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = playerHealth / playerHealthMax;

        // Uppfærir healthbar þegar notandinn verður fyrir meiðslum
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        // Uppfærir healthbar þegar notandinn fær health
        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            frontHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void Death()
    {
        isDead = true;
        playerHealth = 0;
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        lerpTimer = 0f;
        badthing.Play();
    }

    public void RestoreHealth(float healAmount)
    {
        playerHealth += healAmount;
        lerpTimer = 0f;
        goodthing.Play();
    }
}
