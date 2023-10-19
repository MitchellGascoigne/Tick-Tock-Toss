using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using TMPro; 

public class Timer : MonoBehaviour
{
    float countdown = 60; // Initialize a countdown timer with a value of 60 seconds.
    public TMP_Text countNumbers; // Reference to a TextMesh Pro text component for displaying the countdown.

    // Start is called before the first frame update
    void Start()
    {
        // The Start method is empty, so nothing specific happens at the start of the script execution.
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0) // Check if the countdown timer is greater than 0.
        {
            countdown -= Time.deltaTime; // Decrement the countdown by the time passed in the current frame.
        }

        double b = System.Math.Round(countdown, 2); // Round the countdown value to two decimal places and store it in a double variable 'b'.
        countNumbers.text = b.ToString(); // Update the TextMesh Pro text component with the rounded countdown value as a string.
    }
}
