using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    
    void Start()
    {
        SelectButton(sprite1);
        sprite1.onClick.AddListener(() => OnButtonPressed(sprite1));
        sprite2.onClick.AddListener(() => OnButtonPressed(sprite2));
        startGame.onClick.AddListener(StartGame);
        goHome.onClick.AddListener(GoHome);
        nameInput.onValueChanged.AddListener(name =>
        {
            playerName = name;
            startGame.interactable = (playerName.Length >= 3);
        });
    }
    
    void StartGame()
    {
        if (!string.IsNullOrEmpty(playerName) && playerName.Length >= 3) // Check if the name is not empty
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


