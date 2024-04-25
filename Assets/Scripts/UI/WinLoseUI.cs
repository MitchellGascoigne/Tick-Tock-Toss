using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] Canvas loseCanvas;
    [SerializeField] Canvas winCanvas;

    void OnEnable()
    {
        PlayerManager.OnLocalPlayerDeath += OnLocalPlayerDeath;
        PlayerManager.OnLocalPlayerSpawn += OnLocalPlayerSpawn;

        loseCanvas.enabled = false;
        winCanvas.enabled = false;
    }

    void OnDisable()
    {
        PlayerManager.OnLocalPlayerDeath -= OnLocalPlayerDeath;
        PlayerManager.OnLocalPlayerSpawn -= OnLocalPlayerSpawn;
    }

    void OnLocalPlayerDeath(Vector3 position)
    {
        if (IsLastPlayerStanding())
        {
            winCanvas.enabled = true;
            Cursor.visible = true; 
        }
        else
        {
            loseCanvas.enabled = true;
            Cursor.visible = true; 
        }
    }

    void OnLocalPlayerSpawn(GameObject playerObject, Vector3 position)
    {
        loseCanvas.enabled = false;
        winCanvas.enabled = false;
        Cursor.visible = false; 
    }

    bool IsLastPlayerStanding()
    {
        return PhotonNetwork.PlayerList.Length == 1;
    }
}
