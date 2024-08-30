using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.ComponentModel; // Import TextMeshPro for UI text handling

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText; // Reference to UI text for displaying score
    public TextMeshProUGUI livesText;
    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void AddLife(int live)
    {
    live=1;
        lives=lives+live;
        UpdateLivesUI();
           
        
    }
    public void LoseLife()
    {
    
            lives--;
            UpdateLivesUI();
            if (lives <= 0)
            {
            // Game over logic (reset the game or show game over screen)
                GameOver();
            }
            else
            {
            // Reset Pac-Man's position (implement this in PacManController)
                PacmanController.instance.ResetPosition();
            }
        
    }
     public void GameOver()
       {
           // Add game over logic, such as showing a game over screen or restarting the level.
           // For simplicity, this example reloads the current scene.
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
    public void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;

    }
     public void QuitGame()
    {
        Application.Quit();
    }

}