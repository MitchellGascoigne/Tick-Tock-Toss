using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

// Shows a timer above the targeted player's head.
public class TimerOverhead : MonoBehaviourPun
{
    [SerializeField] GameObject visuals;
    [SerializeField] TMP_Text timerText;
    bool visualsEnabled;

    float currentTimer;
    Player currentTarget;

    // This is how this script begins "listening" to changes to the timer and target.
    void OnEnable ()
    {
        // Kind of crude; set visualsEnabled to true so that DisableVisuals will run, even though visualsEnabled starts off as false.
        visualsEnabled = true;
        DisableVisuals();

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

        UpdateVisuals();
    }

    void OnCurrentTargetChanged (Player newTarget)
    {
        currentTarget = newTarget;

        UpdateVisuals();
    }

    void UpdateVisuals ()
    {
        if (currentTarget == null)
            return;

        bool targeted = currentTarget == photonView.Controller;

        if (targeted)
        {
            EnableVisuals();

            timerText.text = (Mathf.Round(currentTimer * 10f) / 10f) + "s";
        }
        else
        {
            DisableVisuals();
        }
    }

    void EnableVisuals ()
    {
        if (visualsEnabled)
            return;

        visuals.SetActive(true);
        visualsEnabled = true;
    }

    void DisableVisuals ()
    {
        if (!visualsEnabled)
            return;

        visuals.SetActive(false);
        visualsEnabled = false;
    }
}
