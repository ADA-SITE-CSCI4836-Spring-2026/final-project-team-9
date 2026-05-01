using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject pauseUI;
    public GameObject winUI;
    public GameObject loseUI;

    public bool isPaused = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            if(isPaused) PauseGame();
            if(!isPaused) ResumeGame();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        winUI.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        loseUI.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}