using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float countdown = 10; // Initialize a countdown timer with a value of 10 seconds.
    public TMP_Text countNumbers; // Reference to a TextMeshPro text component for displaying the countdown.
    public GameObject player; // Reference to the GameObject to disable when the countdown reaches zero.

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0) // Check if the countdown timer is greater than 0.
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
