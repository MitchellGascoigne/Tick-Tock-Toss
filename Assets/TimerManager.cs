using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using Photon.Pun; 
using Photon.Realtime; 

public class TimerManager : MonoBehaviourPunCallbacks
{
    public GameObject timerPrefab; // Reference to the timer prefab that will be instantiated.
    private bool timerAssigned = false; // A flag to track whether a timer has been assigned.

    void Start()
    {
        if (!timerAssigned && PhotonNetwork.IsMasterClient) // Check if a timer hasn't been assigned and the local player is the master client.
        {
            AssignTimerToRandomPlayer(); // Call a method to assign a timer to a random player.
            timerAssigned = true; // Set the flag to indicate that a timer has been assigned.
        }
    }

    void AssignTimerToRandomPlayer()
    {
        PhotonView[] playerViews = FindObjectsOfType<PhotonView>(); // Find all PhotonViews in the scene (representing networked objects).
        List<PhotonView> playersWithoutTimer = new List<PhotonView>(); // Create a list to store players without timers.

        foreach (PhotonView playerView in playerViews)
        {
            if (playerView.IsMine && playerView.gameObject != this.gameObject) // Check if a PhotonView represents the local player and is not this game object.
            {
                playersWithoutTimer.Add(playerView); // Add the player to the list of players without timers.
            }
        }

        if (playersWithoutTimer.Count > 0) // Check if there are players without timers.
        {
            int randomIndex = Random.Range(0, playersWithoutTimer.Count); // Generate a random index to select a player from the list.
            PhotonView randomPlayer = playersWithoutTimer[randomIndex]; // Get a random player from the list.

            Vector3 timerPosition = randomPlayer.transform.position + Vector3.up * 2; // Calculate a position for the timer above the selected player.
            GameObject timer = Instantiate(timerPrefab, timerPosition, Quaternion.identity); // Instantiate the timer prefab at the calculated position.

            timer.GetPhotonView().TransferOwnership(randomPlayer.Owner); // Transfer ownership of the timer to the owner of the selected player.
        }
    }
}
