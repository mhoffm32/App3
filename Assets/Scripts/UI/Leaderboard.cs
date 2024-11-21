using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    public string mainMenu;

    public void BackToMenu() {
        Debug.Log("Back to menu");
        SceneManager.LoadScene(mainMenu);
    }
}
