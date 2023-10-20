using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    public GameObject playerPrefab;
    public GameObject timerPrefab;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Debug.Log("Instantiated Player Controller");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);

        // After instantiating the player controller, check if it's the local player.
        if (PV.IsMine)
        {
            RequestTimer();
        }
    }

    void RequestTimer()
    {
        PV.RPC("AssignTimerToPlayer", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void AssignTimerToPlayer()
    {
        if (PV.IsMine)
        {
            Vector3 timerPosition = transform.position + Vector3.up * 2;
            GameObject timer = Instantiate(timerPrefab, timerPosition, Quaternion.identity);

            // Transfer ownership of the timer to another player (not the host)
            PhotonView[] playerViews = FindObjectsOfType<PhotonView>();
            List<PhotonView> players = new List<PhotonView>();

            foreach (PhotonView playerView in playerViews)
            {
                if (playerView != PV && playerView.IsMine)
                {
                    players.Add(playerView);
                }
            }

            if (players.Count > 0)
            {
                int randomIndex = Random.Range(0, players.Count);
                timer.GetPhotonView().TransferOwnership(players[randomIndex].Owner);
            }
        }
    }
}
