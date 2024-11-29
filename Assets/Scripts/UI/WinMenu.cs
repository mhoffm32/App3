using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// script to manage win scene

public class WinMenu : MonoBehaviour
{
    // UI variables
    public TextMeshProUGUI timeToBeatText;

    public void Start()
    {
        UpdateMenuText();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //SubmitScore();
            SceneManager.LoadScene("MainMenu");
        }
    }

    // method to update the menu text based on game data
    void UpdateMenuText()
    {
        // display the number of enemies killed and time to beat the level
        timeToBeatText.text = Math.Round(GameData.timer/60, 2) + " minutes";
    }

    // method to submit the player's score and name
    public void SubmitScore()
    {
        // uncomment once player name is implemented into GameData script
        //string playerName = GameData.playerName;
        
        // calculate score by simply rounding timer to 2 decimal places
        float score = (float)Math.Round(GameData.timer/60, 2);

        // submit to leaderboard
        SubmitToLeaderboard(GameData.PlayerName, score);
    }

    // method to take submitted score and name and put onto leaderboard
    void SubmitToLeaderboard(string name, float score)
    {
        // load data
        GameData gameData = GameData.LoadData();

        // add player record to leaderboard
        gameData.AddPlayerRecord(name, score);
        // save data
        gameData.SaveData();

        // load main menu after submitting score
        Debug.Log($"Player {name} with score {score} added to leaderboard.");
        SceneManager.LoadScene("MainMenu");
    }
}