using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BouncingText : MonoBehaviour
{
    public float bounceHeight = 10f;
    public float bounceSpeed = 1f;

    private Vector3 startPos;
    private float timeElapsed;

    private TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();  // Get the TextMeshPro component attached to this GameObject
        startPos = transform.position;   // Store the starting position
    }

    void Update()
    {
        // Update the time elapsed
        timeElapsed += Time.deltaTime * bounceSpeed;

        // Calculate the new position with a sine wave for the bouncing effect
        float yOffset = Mathf.Sin(timeElapsed) * bounceHeight;

        // Apply the new position
        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }
}
