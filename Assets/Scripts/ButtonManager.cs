using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.RestartLevel();
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; // important
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        Debug.Log("dsskdfldskfjkd");
        SceneManager.LoadScene("Level 2");
    }
}