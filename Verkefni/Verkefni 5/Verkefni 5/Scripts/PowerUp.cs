using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        StatBoost,
        Points,
        MorePoints
    }
    public GameObject player;
    private GameManager gameManager;

    public AudioSource audioSource;
    public AudioClip boostClip;
    public PowerUpType type;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }   
    public void Apply(GameObject player)
    {
        switch (type)
        {
            case PowerUpType.StatBoost:
                player.GetComponent<Player>().BoostStats();
                break;
            case PowerUpType.Points:
                gameManager.AddScore(25);
                break;
            case PowerUpType.MorePoints:
                gameManager.AddScore(50);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Apply(other.gameObject); 
            audioSource.PlayOneShot(boostClip);
            Destroy(gameObject);
        }
    }

}
