using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine;

public class TimerManager : MonoBehaviourPun
{
    public GameObject timerPrefab;  // Reference to the timer display prefab to be instantiated.
    public float timerDuration = 10f;

    void Start()
    {
        // Check if this is the master client to avoid conflicts.
        if (PhotonNetwork.IsMasterClient)
        {
            // Get a list of all players in the current room.
            Player[] allPlayers = PhotonNetwork.PlayerList;

            // Choose a random player in the room.
            if (allPlayers.Length > 0)
            {
                int randomIndex = Random.Range(0, allPlayers.Length);
                Player randomPlayer = allPlayers[randomIndex];

                // Check if this player is the local player.
                if (randomPlayer == PhotonNetwork.LocalPlayer)
                {
                    // Instantiate the timer prefab for the randomly selected player.
                    InstantiateTimerAbovePlayer(randomPlayer);
                }
            }
        }
    }

    public void InstantiateTimerAbovePlayer(Player player)
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference is null. Cannot instantiate timer.");
            return;
        }

        GameObject playerGameObject = PhotonView.Find(player.ActorNumber).gameObject;
        if (playerGameObject == null)
        {
            Debug.LogWarning("Player GameObject not found. Cannot instantiate timer.");
            return;
        }

        // Calculate the position above the player's head.
        Vector3 playerPosition = playerGameObject.transform.position;
        float heightAboveHead = 2.0f; // Adjust this value as needed.
        Vector3 timerPosition = new Vector3(playerPosition.x, playerPosition.y + heightAboveHead, playerPosition.z);

        // Instantiate the timer prefab at the calculated position with no rotation.
        PhotonNetwork.Instantiate("TimerPrefab", timerPosition, Quaternion.identity);
        Debug.Log("Timer Display Instantiated above the player's head at " + timerPosition);
    }
}
