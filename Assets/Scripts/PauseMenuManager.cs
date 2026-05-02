using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// =====================================================
//  PauseMenuManager.cs
//  Attach to: an empty GameObject called "PauseManager"
//  in your Gameplay scene
// =====================================================

public class PauseMenuManager : MonoBehaviour
{
    [Header("Pause UI")]
    public GameObject pauseUI;         // drag your PauseUI GameObject here

    [Header("Scene Names")]
    public string mainMenuScene = "MainMenu";  // your main menu scene name

    private bool isPaused = false;

    // ── Unity lifecycle ───────────────────────────────────────────────────────

    void Start()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    // ── Pause / Resume ────────────────────────────────────────────────────────

    void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // ── Button callbacks ──────────────────────────────────────────────────────

    public void OnResumeButton()
    {
        ResumeGame();
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void OnQuitButton()
    {
        Time.timeScale = 1f;
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}