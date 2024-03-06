using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LockAtiveRoom : MonoBehaviourPunCallbacks
{
    // Reference to the room object
    public Room room;

    // Start is called before the first frame update
    void Start()
    {
        // Check if this client is the master client
        if (PhotonNetwork.IsMasterClient)
        {
            // Ensure that 'room' is assigned in the Unity Editor
            if (room != null)
            {
                // Set the IsOpen property of the room to false
                room.IsOpen = false;
            }
            else
            {
                Debug.LogError("Cannot Enter Active Room");
            }
        }
    }
}
