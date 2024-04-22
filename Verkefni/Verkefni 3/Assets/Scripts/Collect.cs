using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public GameManager GameManager;
    public int Score = 10;

    public void Start()
    {
        // Sækir GameManager script
        GameManager = GameManager.GetComponent<GameManager>();
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log(message:"Collecting Jewel!");
        GameManager.UpdateScore(Score);
        Destroy(gameObject);
        return true;
    }
}
