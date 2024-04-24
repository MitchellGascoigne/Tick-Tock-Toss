//using UnityEngine;
//using static PlayerManager; // Add this using directive

//public class SpectatorMode : MonoBehaviour
//{
//    private void Start()
//    {
//        // Start spectating a random active player
//        StartCoroutine(SpectateRandomPlayer());
//    }

//    private IEnumerator SpectateRandomPlayer()
//    {
//        while (true)
//        {
//            // Wait for a short delay before selecting a new player to spectate
//            yield return new WaitForSeconds(5f);

//            // Find all player GameObjects in the scene
//            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

//            // Filter out active players (customize this condition based on your game logic)
//            GameObject[] activePlayers = FilterActivePlayers(players);

//            // If there are active players, randomly select one to spectate
//            if (activePlayers.Length > 0)
//            {
//                GameObject playerToSpectate = activePlayers[Random.Range(0, activePlayers.Length)];

//                // Lock the camera onto the selected player's camera
//                LockCameraToPlayer(playerToSpectate);
//            }
//        }
//    }

//    private GameObject[] FilterActivePlayers(GameObject[] players)
//    {
//        // Example condition: Check if the player has a PlayerManager component and is alive
//        // You should customize this condition based on your game logic
//        return System.Array.FindAll(players, player => player.GetComponent<PlayerManager>() != null && player.GetComponent<PlayerManager>().IsAlive);
//    }

//    private void LockCameraToPlayer(GameObject player)
//    {
//        // Implementation to lock the camera to the selected player
//    }
//}
