using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class TimerManager : MonoBehaviourPunCallbacks
{
    public GameObject timerPrefab; // Reference to the timer prefab that will be instantiated.

    private GameObject timerInstance; // The instance of the timer prefab.
    private bool timerAssigned = false; // A flag to track whether a timer has been assigned.

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("AssignTimerToRandomPlayer", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void AssignTimerToRandomPlayer()
    {
        if (!timerAssigned)
        {
            PhotonView[] playerViews = FindObjectsOfType<PhotonView>();
            List<PhotonView> playersWithoutTimer = new List<PhotonView>();

            foreach (PhotonView playerView in playerViews)
            {
                if (playerView.IsMine)
                {
                    playersWithoutTimer.Add(playerView);
                }
            }

            if (playersWithoutTimer.Count > 0)
            {
                int randomIndex = Random.Range(0, playersWithoutTimer.Count);
                PhotonView randomPlayer = playersWithoutTimer[randomIndex];

                Vector3 timerPosition = randomPlayer.transform.position + Vector3.up * 2;
                timerInstance = Instantiate(timerPrefab, timerPosition, Quaternion.identity);

                timerInstance.transform.SetParent(randomPlayer.transform); // Set timer as a child of the player.
                timerAssigned = true;
            }
        }
    }
}
