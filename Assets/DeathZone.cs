using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Vector3 playerStartPosition; // Set this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable the player object
            other.gameObject.SetActive(false);

            // Re-enable the player at the specified starting position
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        // Set the player's position to the starting position
        player.transform.position = playerStartPosition;

        // Re-enable the player object
        player.SetActive(true);
    }
}
