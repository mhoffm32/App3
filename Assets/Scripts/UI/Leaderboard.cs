using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// script to manage leaderboard
public class Leaderboard : MonoBehaviour
{
    public TMP_Text score1, score2, score3, player1, player2, player3;
    public string mainMenuScene;

    // Sound-related fields
    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip clickSound;    // Assign this in the Inspector
    public float delayDuration = 0.5f; // Time (in seconds) to wait before performing actions

    void Start()
    {
        LoadLeaderboard();
    }

    // to load leaderboard data
    void LoadLeaderboard()
    {
        GameData gameData = GameData.LoadData();

        // Debugging: Log leaderboard
        foreach (var record in gameData.leaderboard)
        {
            Debug.Log($"Player: {record.playerName}, Time: {record.beatTime}");
        }
        // ensure the leaderboard is sorted by score (ascending order)
        gameData.leaderboard.Sort((a, b) => a.beatTime.CompareTo(b.beatTime));

        // display top 3 scores if available
        if (gameData.leaderboard.Count > 0)
        {
            player1.text = gameData.leaderboard[0].playerName;
            score1.text = gameData.leaderboard[0].beatTime.ToString() + " seconds";
        }
        else
        {
            player1.text = "---";
            score1.text = "---";
        }

        if (gameData.leaderboard.Count > 1)
        {
            player2.text = gameData.leaderboard[1].playerName;
            score2.text = gameData.leaderboard[1].beatTime.ToString() + " seconds";
        }
        else
        {
            player2.text = "---";
            score2.text = "---";
        }

        if (gameData.leaderboard.Count > 2)
        {
            player3.text = gameData.leaderboard[2].playerName;
            score3.text = gameData.leaderboard[2].beatTime.ToString() + " seconds";
        }
        else
        {
            player3.text = "---";
            score3.text = "---";
        }
    }

    // Play the click sound
    private void PlaySound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    // to clear all data from leaderboard
    public void ClearAllData()
    {
        PlaySound(); // Play the click sound

        // clear PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // clear in-memory leaderboard data by resetting the GameData instance
        GameData gameData = new GameData();
        gameData.SaveData();

        // clear the UI
        player1.text = "---";
        score1.text = "---";
        player2.text = "---";
        score2.text = "---";
        player3.text = "---";
        score3.text = "---";

        Debug.Log("All data cleared.");    }

    // to return to the main menu
    public void Back()
    {
        StartCoroutine(BackWithDelay());
    }

    private IEnumerator BackWithDelay()
    {
        PlaySound(); // Play the click sound
        yield return new WaitForSeconds(delayDuration); // Wait for the delay
        SceneManager.LoadScene(mainMenuScene); // Switch scene
    }
}
