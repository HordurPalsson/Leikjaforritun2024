using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    // Breytur
    public float score = 100;
    public TextMeshProUGUI scoreText;
    public static GameManager instance;
    public List<GameObject> gameObjectsToDisable;

    private bool isGameActive = false;
    private float gameTimer;
    public TextMeshProUGUI timerText;
    private float scoreDecreaseTimer = 0;
    
    public AudioSource audioSource;
    public AudioClip death;

    public GameObject animationImage;
    public GameObject titleScreenImage;
    public GameObject startScreen;
    public GameObject endScreen;
    public TextMeshProUGUI timeLastedText;
    public float currentTime;

    void Start()
    {
        SetActiveAll(gameObjectsToDisable, false);
        titleScreenImage.SetActive(false);
        animationImage.SetActive(true); 
        endScreen.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {   
        // Upfærir timer á meðan leikurinn er í gangi
        if (isGameActive)
        {
            UpdateTimer();
            currentTime = gameTimer;
            DecreaseScoreOverTime();
        }
    }

    // Byrjar leikinn
    public void StartGame()
    {
        isGameActive = true;
        gameTimer = 0;
        UpdateScoreUI();
        SetActiveAll(gameObjectsToDisable, true);
        startScreen.SetActive(false);
        Cursor.visible = false;
    }

    // Stoppar leikinn
    public void EndGame()
    {
        isGameActive = false;
        SetActiveAll(gameObjectsToDisable, false);
        endScreen.SetActive(true); 
        timeLastedText.text = "Time: " + currentTime.ToString();
        audioSource.PlayOneShot(death);
    }

    // upfærir skeiðklukkuna
    private void UpdateTimer()
    {
        gameTimer += Time.deltaTime;
        UpdateTimerDisplay(gameTimer);
    }

    // upfærir skeiðklukku textan
    private void UpdateTimerDisplay(float time)
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(time);
        }
    }

    private float scoreDecrease = 10;
    private float increaseScoreDecrease = 10;
    public float scoreDecreaseInterval = 30.0f;

    // Lækar stig hverja sekúndu
    private void DecreaseScoreOverTime()
    {
        scoreDecreaseTimer += Time.deltaTime;
        if (scoreDecreaseTimer >= 1f) // Kíkir hvort sek hefur liðið
        {
            score -= scoreDecrease; // Lækkar stig 
            scoreDecreaseTimer = 0; // Enduræsir timer
            UpdateScoreUI();

            if (score <= 0)  // Kíkir hvort stig eru 0
            {
                EndGame();
            }
        }
        // Hækkar magnið sem leikmaður missir á sekundu hverjar 30 sekundur
        if (Time.deltaTime >= scoreDecreaseInterval)
        {
            scoreDecreaseInterval += 30;
            scoreDecrease *= increaseScoreDecrease;
            Debug.Log("Increased penalty");
        }
    }

    // Skeið klukka
    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Bætir við stigum
    public void AddScore(float points)
    {
        score += points; 
        UpdateScoreUI();
    }

    // lækar stig
    public void DecreaseScore(float points)
    {
        score -= points; 
        UpdateScoreUI();
    }

    // Uppfærir stigatalið
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString(); 
    }

    // Slekkur eða kveikir á objects í senunni
    private void SetActiveAll(List<GameObject> objects, bool isActive)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
                obj.SetActive(isActive);
        }
    }
    
    public void ShowTitleScreen()
    {
        animationImage.SetActive(false);  // Felur anim
        titleScreenImage.SetActive(true);  // Sýnir title screen
    }

    // Endurræsir leikinn
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Slekkur á leiknum
    public void Quit()
    {
        Application.Quit();
    }
}
