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
            KillPlayer(other.gameObject);
        }
    }

    public void KillPlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().Die();
    }
}
