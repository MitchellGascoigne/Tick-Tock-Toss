using UnityEngine;
using Photon.Pun;

public class TimerManager : MonoBehaviourPun
{
    public GameObject timerPrefab;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PlayerController[] players = FindObjectsOfType<PlayerController>();
            if (players.Length > 0)
            {
                int randomIndex = Random.Range(0, players.Length);
                PlayerController randomPlayer = players[randomIndex];

                randomPlayer.GainTimer(); // Instantiate the timer on a random player.
            }
        }
    }
}
