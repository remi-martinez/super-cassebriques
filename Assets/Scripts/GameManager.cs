using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour // Gestion globale du jeu
{
    // Script balle
    public Ball ballScript;

    // Coeurs pour la vie
    public GameObject panel;
    public int lives = 3;
    public int maxLives = 10;
    private RawImage[] heartImages;

    // Score
    public int score = 0;
    public TextMeshProUGUI scoreLabel;
    

    // GameOver
    public bool gameOver;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreText;
    public GameObject newHighScore;

    // Pause
    public bool gamePaused;
    public GameObject pausePanel;

    // Prochain niveau
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = -1;
    public GameObject nextLevelText;
    
    void Start()
    {
        heartImages = panel.GetComponentsInChildren<RawImage>();
        Array.Reverse(heartImages);
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        LoadLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Mettre le jeu en pause
        {
            SetPause(!gamePaused);
        }
    }

    public void SetPause(bool state) // Mettre le jeu en pause
    {
        pausePanel.SetActive(state);
        Time.timeScale = (state ? 0f : 1f);
        gamePaused = state;
    }

    public void RemoveLive() // Retirer une vie au joueur
    {
        lives--;
        if(lives > 0)
        {
            heartImages[lives].enabled = false; // Actualiser les sprites de vie
        } else
        {
            lives = 0;
            heartImages[0].enabled = false;
            GameOver();
        }
    }

    public void AddLive() // Ajouter une vie au joueur
    {
        if(lives < maxLives)
        {
            heartImages[lives].enabled = true; // Actualiser les sprites de vie
            lives++;
        }
    }

    public void AddScore(int points) // Ajouter du score au joueur
    {
        score += points;
        scoreLabel.text = score.ToString();
    }

    public void UpdateNumberOfBricks() // Met à jour le nb de briques pour vérifier la progression dans le niveau
    {
        numberOfBricks--;
        if(numberOfBricks <= 0)
        {
            gameOver = true;
            if (currentLevelIndex == levels.Length - 1)
            {
                currentLevelIndex = -1; // Si au niveau max : on repasse on niveau 0
            }
            nextLevelText.SetActive(true);
            ballScript.inPlay = false;
            Invoke("LoadLevel", 1f);
        }
    }

    void LoadLevel() // Charger le niveau suivant (boucle infinie)
    {
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity); // Niveau suivant
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
        nextLevelText.SetActive(false);
    }

    public void GameOver() // Game Over car plus de vie
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = this.score.ToString("# ###");
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > highScore)
        {
            newHighScore.SetActive(true); // Label orange "NEW!"
            PlayerPrefs.SetInt("HIGHSCORE", score); // Mettre à jour le score
        }
    }

    public void PlayAgain()
    {
        newHighScore.SetActive(false); // Label orange "NEW!" désactivé
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
