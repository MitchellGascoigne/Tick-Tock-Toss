using UnityEngine;
using static PlayerManager;

public class DeathZone : MonoBehaviour
{
    

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

    public void RespawnPlayer(GameObject player)
    {
        // Set the player's position 
        player.transform.position = new Vector3(0f, 1f, 0f);


        // Re-enable the player object
        player.SetActive(true);
    }
}
