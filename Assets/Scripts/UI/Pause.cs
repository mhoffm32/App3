using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseUI;
    public string mainMenuScene;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                PauseGame();
            }
        }
    }

    public void Resume() {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

        // method for main menu button
    public void BackToMenu() {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void PauseGame() {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
