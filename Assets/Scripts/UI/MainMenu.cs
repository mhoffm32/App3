using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string level;
    public string leaderboard;

    public void StartGame() {
        SceneManager.LoadScene(level);
    }

    public void Leaderboard() {
        SceneManager.LoadScene(leaderboard);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
