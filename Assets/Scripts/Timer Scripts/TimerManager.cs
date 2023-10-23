using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class TimerManager : MonoBehaviourPunCallbacks
{
    public GameObject timerPrefab;
    private GameObject timerInstance;
    private bool timerAssigned = false;

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
                timerInstance.transform.SetParent(randomPlayer.transform);

                timerAssigned = true;
            }
        }
    }
}