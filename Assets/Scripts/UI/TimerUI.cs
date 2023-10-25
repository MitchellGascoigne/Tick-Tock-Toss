using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    float currentTimer;
    Player currentTarget;

    // This is how this script begins "listening" to changes to the timer and target.
    void OnEnable ()
    {
        TimerManager.OnCurrentTimerChanged += OnCurrentTimerChanged;
        TimerManager.OnCurrentTargetChanged += OnCurrentTargetChanged;

        // Don't execute the following code if the TimerManager has not yet been initialised.
        if (!TimerManager.Instance)
            return;
        // Also update the Timer and Target immediately, in case any changes have been missed.
        OnCurrentTimerChanged(TimerManager.CurrentTimer);
        OnCurrentTargetChanged(TimerManager.CurrentTarget);
    }

    // This is how this script stops "listening" to changes to the timer and target.
    void OnDisable ()
    {
        TimerManager.OnCurrentTimerChanged -= OnCurrentTimerChanged;
        TimerManager.OnCurrentTargetChanged -= OnCurrentTargetChanged;
    }

    void OnCurrentTimerChanged (float newTimer)
    {
        currentTimer = newTimer;

        UpdateUI();
    }

    void OnCurrentTargetChanged (Player newTarget)
    {
        currentTarget = newTarget;

        UpdateUI();
    }

    void UpdateUI ()
    {
        // Rounds to the nearest decimal place.
        float time = Mathf.Round(currentTimer * 10f) / 10f;

        // \n makes a new line.
        string text = $"Detonates in {time}s\n"; 
        // This line uses a "ternary operator", which is signified by the question mark '?' . It acts as a form of compact if statement, where the colon ':' is an else statement.
        // So: 'if (value) doThing; else doOtherThing;' becomes 'value ? doThing : doOtherThing;'. It only works for values, you can't use any 'void' function with this.
        text += (currentTarget == null ? "" : $"Currently held by '{currentTarget.NickName}'");

        if (currentTarget != null)
        {
            // If the timer will detonate on you next, turn the text red and add (You). Otherwise, turn the text white.
            timerText.color = currentTarget == PhotonNetwork.LocalPlayer ? Color.red : Color.white;
            text += currentTarget == PhotonNetwork.LocalPlayer ? " (You)" : "";
        }

        timerText.text = text;
    }
}
