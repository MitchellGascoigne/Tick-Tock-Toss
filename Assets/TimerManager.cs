using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TimerManager : MonoBehaviourPunCallbacks
{
    public GameObject timerPrefab;
    private bool timerAssigned = false;

    void Start()
    {
        if (!timerAssigned && PhotonNetwork.IsMasterClient)
        {
            AssignTimerToRandomPlayer();
            timerAssigned = true;
        }
    }

    void AssignTimerToRandomPlayer()
    {
        PhotonView[] playerViews = FindObjectsOfType<PhotonView>();
        List<PhotonView> playersWithoutTimer = new List<PhotonView>();

        foreach (PhotonView playerView in playerViews)
        {
            if (playerView.IsMine && playerView.gameObject != this.gameObject)
            {
                playersWithoutTimer.Add(playerView);
            }
        }

        if (playersWithoutTimer.Count > 0)
        {
            int randomIndex = Random.Range(0, playersWithoutTimer.Count);
            PhotonView randomPlayer = playersWithoutTimer[randomIndex];

            Vector3 timerPosition = randomPlayer.transform.position + Vector3.up * 2;
            GameObject timer = Instantiate(timerPrefab, timerPosition, Quaternion.identity);

            timer.GetPhotonView().TransferOwnership(randomPlayer.Owner);
        }
    }
}
