using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] Canvas deathCanvas;
    [SerializeField] Canvas winCanvas;

    void OnEnable()
    {
        PlayerManager.OnLocalPlayerDeath += OnLocalPlayerDeath;
        PlayerManager.OnLocalPlayerSpawn += OnLocalPlayerSpawn;

        deathCanvas.enabled = false;
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
        }
        else
        {
            deathCanvas.enabled = true;
            UnityEngine.Cursor.visible = true;
        }
    }

    void OnLocalPlayerSpawn(GameObject playerObject, Vector3 position)
    {
        deathCanvas.enabled = false;
        winCanvas.enabled = false;
    }

    bool IsLastPlayerStanding()
    {
        return PhotonNetwork.PlayerList.Length == 1;
    }
}
