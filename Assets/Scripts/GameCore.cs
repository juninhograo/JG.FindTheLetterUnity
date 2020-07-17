using UnityEngine;

public class GameCore : MonoBehaviour
{
    public GameObject PausePanel;
    public static bool IsPaused = false;
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
        Debug.Log("Paused");
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        Debug.Log("Resumed");
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }
}
