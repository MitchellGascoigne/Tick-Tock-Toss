using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    public GameObject playerPrefab;
    public GameObject timerPrefab;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        Debug.Log("Instantiated Player Controller");
        GameObject playerController = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);

        // Optionally, you can store a reference to the player controller for later use or destruction.
        PlayerCollision playerCollision = playerController.GetComponent<PlayerCollision>();
        playerCollision.GainTimer(); // Call GainTimer without passing an argument

    }
}
