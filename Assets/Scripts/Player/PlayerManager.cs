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
    [SerializeField]
    private GameObject[] spawnPositionObjects;

    private void CreateController()
    {
        Debug.Log("Instantiated Player Controller");

        // Call SpawnPosition to select a random spawn position.
        Transform selectedSpawnPosition = SpawnPosition();

        // Instantiate the player's controller GameObject for the local player.
        // The "PhotonPrefabs" folder is used to locate the player prefab.
        // It's placed at the selected spawn position with no rotation.
        GameObject playerController = PhotonNetwork.Instantiate("PlayerController", selectedSpawnPosition.position, Quaternion.identity);
    }

    private Transform SpawnPosition()
    {
        if (spawnPositionObjects.Length == 0)
        {
            Debug.LogError("No spawn positions available.");
            return null;
        }

        int randomIndex = Random.Range(0, spawnPositionObjects.Length);
        return spawnPositionObjects[randomIndex].transform;
    }

}
