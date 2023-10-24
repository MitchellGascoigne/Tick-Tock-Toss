using Photon.Pun;
using UnityEngine;

public class TimerManager : MonoBehaviourPun
{
    public GameObject timerDisplayPrefab;  // Reference to the timer display prefab to be instantiated.

    public void InstantiateTimerAbovePlayer(GameObject playerController)
    {
        if (playerController == null)
        {
            Debug.LogWarning("PlayerController reference is null. Cannot instantiate timer.");
            return;
        }

        // Calculate the position above the player's head.
        Vector3 playerPosition = playerController.transform.position;
        float heightAboveHead = 2.0f; // Adjust this value as needed.
        Vector3 timerPosition = new Vector3(playerPosition.x, playerPosition.y + heightAboveHead, playerPosition.z);

        // Instantiate the timer display prefab at the calculated position with no rotation.
        GameObject timerDisplay = PhotonNetwork.Instantiate(timerDisplayPrefab.name, timerPosition, Quaternion.identity);
        Debug.Log("Timer Display Instantiated above PlayerController's head at " + timerPosition);
    }

    public void DestroyTimer(GameObject timerInstance)
    {
        if (timerInstance != null && timerInstance.GetPhotonView().IsMine)
        {
            // If the timer instance exists and belongs to the local player, destroy it using PhotonNetwork.
            PhotonNetwork.Destroy(timerInstance);
        }
    }

    public void DestroyPlayer(GameObject playerInstance)
    {
        if (playerInstance != null && playerInstance.GetPhotonView().IsMine)
        {
            // If the player instance exists and belongs to the local player, destroy it using PhotonNetwork.
            PhotonNetwork.Destroy(playerInstance);
        }
    }
}
