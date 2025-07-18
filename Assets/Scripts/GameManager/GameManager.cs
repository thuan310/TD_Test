using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private Stats wall;
    [SerializeField] private Stats hero;

    [Header("UI")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private bool gameEnded = false;

    private void Start()
    {
        if (wall != null)
            wall.OnDeath += OnWallDestroyed;
        if (hero != null) 
            hero.OnDeath += OnHeroKilled;

        if (waveManager != null)
        {
            waveManager.OnAllWavesCompleted += OnAllWavesCompleted;
            waveManager.StartWaves(); 
        }

        winScreen?.SetActive(false);
        loseScreen?.SetActive(false);
    }

    private void OnWallDestroyed(GameObject attacker)
    {
        Lose();

    }

    private void OnHeroKilled(GameObject attacker)
    {
        Lose();
    }

    private void OnAllWavesCompleted()
    {
        Win();
    }
    private void Lose()
    {
        if (gameEnded) return;

        gameEnded = true;
        loseScreen?.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Win()
    {
        if (gameEnded) return;

        gameEnded = true;
        winScreen?.SetActive(true);
        Time.timeScale = 0f;
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
