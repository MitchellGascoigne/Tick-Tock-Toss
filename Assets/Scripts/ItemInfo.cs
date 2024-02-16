using Photon.Pun;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    [SerializeField] PlayerController playerController; // Reference to the PlayerController script.

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the tag "Item".
        if (other.CompareTag("Item"))
        {
            // Get the index of the second item (index 1).
            int itemIndex = 1;

            // Equip the second item and destroy the collided object.
            playerController.EquipItem(itemIndex);

            // If using Photon, destroy the item across the network.
            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Destroy(other.gameObject);
            else
                Destroy(other.gameObject);
        }
    }
}