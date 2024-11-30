using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class CreatePlayer : MonoBehaviour
{
    public Button sprite1;
    public Button sprite2;
    public Button startGame;
    public Button goHome;
    public TMP_InputField nameInput;
    private Button selectedButton;
    public string gameScene;
    public string mainMenuScene;
    public int playerNo;
    private string playerName;

    // Sound-related fields
    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip clickSound;    // Assign this in the Inspector

    void Start()
    {
        SelectButton(sprite1);
        sprite1.onClick.AddListener(() => OnButtonPressed(sprite1));
        sprite2.onClick.AddListener(() => OnButtonPressed(sprite2));
        startGame.onClick.AddListener(() => PlaySoundAndExecute(StartGame));
        goHome.onClick.AddListener(() => PlaySoundAndExecute(GoHome));
        nameInput.onValueChanged.AddListener(name =>
        {
            playerName = name;
            startGame.interactable = (playerName.Length >= 3);
        });
    }

    private void PlaySound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    private void PlaySoundAndExecute(System.Action action)
    {
        StartCoroutine(PlaySoundAndRunAction(action));
    }

    private IEnumerator PlaySoundAndRunAction(System.Action action)
    {
        PlaySound(); // Play the sound
        yield return new WaitForSeconds(clickSound.length); // Wait for the sound to finish
        action.Invoke(); // Execute the provided action
    }

    void StartGame()
    {
        if (!string.IsNullOrEmpty(playerName) && playerName.Length >= 3)
        {
            playerName = nameInput.text;
            playerNo = selectedButton == sprite1 ? 1 : 2;
            GameData.PlayerName = playerName;

            Debug.Log("PlayerName: " + playerName);
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.SetInt("PlayerNo", playerNo);
            PlayerPrefs.Save();

            SceneManager.LoadScene(gameScene);
        }
    }

    void SelectButton(Button newSelectedButton)
    {
        sprite1.interactable = newSelectedButton != sprite1; // Enable if not selected
        sprite2.interactable = newSelectedButton != sprite2; // Enable if not selected
        selectedButton = newSelectedButton;
    }

    void OnButtonPressed(Button pressedButton)
    {
        SelectButton(pressedButton);
    }

    void GoHome()
    {
        Debug.Log("GoHome");
        SceneManager.LoadScene(mainMenuScene);
    }
}
