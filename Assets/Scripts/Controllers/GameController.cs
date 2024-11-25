using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool isGameOver = false;
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
        
    }
}
