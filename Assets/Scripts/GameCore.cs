using UnityEngine;
using UnityEngine.SceneManagement;
public class GameCore : MonoBehaviour
{
    public GameObject FinishPanel;
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public GameObject Key;

    private bool IsPaused = false;
    private bool IsFinished = false;
    private bool IsGameOver = false;

    //audios clips
    private AudioSource[] audioSource;
    private AudioSource audioGameTheme;
    private AudioSource audioGameOverTheme;
    private AudioSource audioFinishTheme;
    private AudioSource audioKeyUI;
    private AudioSource audioCoin;
    private AudioSource audioJump;
    private AudioSource audioHurt;

    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        audioGameTheme = audioSource[0];
        audioFinishTheme = audioSource[1];
        audioGameOverTheme = audioSource[2];
        audioKeyUI = audioSource[3];
        audioCoin = audioSource[4];
        audioJump = audioSource[5];
        audioHurt = audioSource[6];
        audioGameTheme.Play();
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
        if (!IsFinished && !IsGameOver)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
            IsPaused = true;
            audioGameTheme.Pause();
        }
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        audioGameTheme.Play();
    }
    public void Finish()
    {
        FinishPanel.SetActive(true);
        Time.timeScale = 0f;
        IsFinished = true;
        audioFinishTheme.Play();
    }
    public void ShowKeyUI()
    {
        Key.SetActive(true);
        Time.timeScale = 1f;
        IsFinished = false;
        IsPaused = false;
        IsGameOver = false;
        audioKeyUI.Play();
    }
    public void PlayJumpAudio()
    {
        if (audioJump != null)
            audioJump.Play();
    }
    public void PlayGetCoinAudio()
    {
        if (audioCoin != null)
            audioCoin.Play();
    }
    public void PlayGetHurtAudio()
    {
        if (audioHurt != null)
            audioHurt.Play();
    }
    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        IsGameOver = true;
        audioGameTheme.Stop();
        audioGameOverTheme.Play();
    }
    public void Restart()
    {
        PausePanel.SetActive(false);
        Key.gameObject.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioGameOverTheme.Stop();
        audioGameTheme.Play();
    }
}
