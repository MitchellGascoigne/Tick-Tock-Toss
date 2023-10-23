using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float countdown = 10f; // Initialize a countdown timer with a value of 10 seconds.
    public TMP_Text countNumbers; // Reference to a TextMeshPro text component for displaying the countdown.

    void Start()
    {
        if (countNumbers == null)
        {
            Debug.LogError("countNumbers is not assigned. Please assign it in the Inspector.");
            enabled = false; // Disable the script to prevent further errors.
        }
    }

    void Update()
    {
        if (countNumbers != null)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime; // Decrement the countdown by the time passed in the current frame.
            }
            else
            {
                // DESTROY TIMER
                // DESTROY PLAYER
            }

            double b = System.Math.Round(countdown, 2); // Round the countdown value to two decimal places and store it in a double variable 'b'.
            countNumbers.text = b.ToString(); // Update the TextMeshPro text component with the rounded countdown value as a string.
        }
    }
}

