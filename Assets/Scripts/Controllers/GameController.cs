using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool isGameOver = false;
    public static bool isWon = false;
    public bool isPaused = false;
    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && !isPaused)
        {
            timer += Time.deltaTime;
        }
        if (isGameOver) {
            timer = Mathf.Round(timer * 100f) / 100f;
            GameOver();
        }
        if (isWon) {
            timer = Mathf.Round(timer * 100f) / 100f;
            Win();
        }
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
        isGameOver = false;
    }

    public void Win() {
        GameData.timer = timer;
        SceneManager.LoadScene("Win");
        isWon = false;
    }
}
