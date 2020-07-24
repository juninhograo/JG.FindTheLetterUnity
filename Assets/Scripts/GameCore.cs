using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{
    public GameObject FinishPanel;
    public GameObject FindTheKeyPanel;
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public GameObject Key;
    public Text txtFinalMessage;

    private bool IsPaused = false;
    private bool IsFinished = false;
    private bool IsShowFindKeyMessage = false;
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
    private AudioSource audioAlertTheme;
    private AudioSource audioSelectOption;

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
        audioAlertTheme = audioSource[7];
        audioSelectOption = audioSource[8];
        audioGameTheme.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused || IsShowFindKeyMessage)
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
        FindTheKeyPanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        IsShowFindKeyMessage = false;
        audioGameTheme.Play();
    }
    public void Finish()
    {
        FinishPanel.SetActive(true);
        Time.timeScale = 0f;
        IsFinished = true;
        audioGameTheme.Pause();
        audioFinishTheme.Play();
    }
    public void FindTheKeyMessage(bool showMessage)
    {
        FindTheKeyPanel.SetActive(showMessage);
        Time.timeScale = 0f;
        IsShowFindKeyMessage = true;
        audioGameTheme.Pause();
        audioAlertTheme.Play();
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
        audioGameTheme.Pause();
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
        audioGameOverTheme.Pause();
        audioGameTheme.Play();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void WrongAnswer()
    {
        Answer(false);
    }
    public void RightAnswer()
    {
        Answer(true);
    }
    private void Answer(bool right)
    {
        if (right)
        {
            txtFinalMessage.text = "Yes! Right answer!";
            PlayGetCoinAudio();
        }
        else
        {
            txtFinalMessage.text = "No! Wrong answer!";
            PlayGetHurtAudio();
        }
            
    }
}
