using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private GameManager gameManager;
    private int currentScore;
    private int coinsCollected;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Scene currentScene = SceneManager.GetActiveScene();

        int buildIndex = currentScene.buildIndex;

        

		// Check the scene name as a conditional.
		switch (buildIndex)
		{
		case 1:
			gameManager.isGameActive = true;
            gameManager.UpdateScore(currentScore);
            gameManager.CoinsCollected(coinsCollected);
			break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            coinsCollected = gameManager.coins;
            currentScore = gameManager.score;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
}
