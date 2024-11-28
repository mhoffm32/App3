using UnityEngine;
using UnityEngine.SceneManagement;

// script to manage gameover scene

public class GameOverMenu : MonoBehaviour
{  
    // method to restart the game
    public void Restart() {
        SceneManager.LoadScene("Level_1");
    }

    // method to go back to main menu
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}