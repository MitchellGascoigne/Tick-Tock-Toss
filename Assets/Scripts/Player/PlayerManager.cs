using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;           // Reference to the PhotonView component attached to this GameObject.
    public GameObject playerPrefab;  // The player's prefab to be instantiated for each player.

    private void Awake()
    {
        PV = GetComponent<PhotonView>();  // Get the PhotonView component on this GameObject.
    }

    private void Start()
    {
        if (PV.IsMine)  // If this GameObject belongs to the local player...
        {
            CreateController();  // ...create the player's controller.
        }
    }

    private void CreateController()
    {
        Debug.Log("Instantiated Player Controller");  // Log a message to the console.

        // Instantiate the player's controller GameObject for the local player.
        // The "PhotonPrefabs" folder is used to locate the player prefab.
        // It's placed at position (0, 0, 0) with no rotation.
        Vector3 spawnPosition = new Vector3(0f, 1f, 0f); // Set your desired position here
        GameObject playerController = PhotonNetwork.Instantiate("PlayerController", spawnPosition, Quaternion.identity);

    }


}
