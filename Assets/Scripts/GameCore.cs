using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public static bool IsPaused = false;
    public static bool IsGameOver = false;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        Debug.Log("Pause");
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        Debug.Log("Resume");
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        IsGameOver = true;
    }

    public void Restart()
    {
        Debug.Log("Restart");
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
