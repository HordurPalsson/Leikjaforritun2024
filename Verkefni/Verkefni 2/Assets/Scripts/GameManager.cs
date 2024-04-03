using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsCollected;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject gameOver;
    public GameObject Player;
    public bool isGameActive = false;
    public int score;
    public int coins;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame() {
        score = 0;
        UpdateScore(0);
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
    }

    public void GameOver() {
        isGameActive = false;
        gameOver.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void CoinsCollected(int coinsToAdd) {
        coins += coinsToAdd;
        coinsCollected.text = "Coins: " + coins;
    }

    public void RestartGame() {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
