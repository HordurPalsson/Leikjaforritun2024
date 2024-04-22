using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // breytur
    public GameObject gameOverScreen;
    public GameObject Player;
    public Player PlayerScript;
    public int Score;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        Score = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    void Update()
    {
        // Ef leikmaðurinn deyr
        if (PlayerScript.isDead)
        {
            OnDeath();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Uppfærir stig
    public void UpdateScore(int points)
    {
        Score += points;
        scoreText.text = "Score: " + Score;
        Debug.Log(Score);
        PlayerScript.RestoreHealth(points);
    }

    // Stoppar leikinn þegar leikmaður deyr
    public void OnDeath()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }  

    // Slekkur á leiknum

    public void Quit()
    {
        Application.Quit();
    }

    // Lætur senunana byrja upp á nýtt
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
