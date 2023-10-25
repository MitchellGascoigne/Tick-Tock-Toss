using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

// This script is on every player prefab. All players run the visual code. The MasterClient runs detonation code. The PhotonView Controller actually detonates.
// Handles detonation and detonation visuals.
public class PlayerDetonation : MonoBehaviourPun
{
    [SerializeField] PlayerController controller;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject visuals;
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

        CheckDetonation();
    }

    void OnCurrentTargetChanged (Player newTarget)
    {
        currentTarget = newTarget;

        CheckDetonation();
    }

    void CheckDetonation ()
    {
        if (currentTarget == null)
            return;

        bool targeted = currentTarget == photonView.Controller;

        if (targeted)
        {
            EnableVisuals();
        } else
        {
            DisableVisuals();
        }

        if (targeted && currentTimer <= 0)
        {
            Detonate();
        }
    }

    void Detonate ()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        TimerManager.Instance.PlayerDetonated();
        // Send the Detonate RPC to everyone. The controller of the PhotonView will die, and all clients will spawn particles on their end rather than instantiating particles on the network.
        photonView.RPC(nameof(RPC_Detonate), RpcTarget.All);
    }

    [PunRPC]
    void RPC_Detonate (PhotonMessageInfo info)
    {
        // Don't execute this code if it's not from the MasterClient or yourself.
        if (info.Sender != PhotonNetwork.MasterClient && info.Sender != PhotonNetwork.LocalPlayer)
            return;

        // Create particles while also scheduling to destroy them in 10 seconds.
        Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 10f);

        // Only die if this is your photon view.
        if (PhotonNetwork.LocalPlayer != photonView.Controller)
            return;

        controller.Die();
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
