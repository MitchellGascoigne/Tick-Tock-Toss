using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathUI : MonoBehaviour
{
    [SerializeField] Canvas target;
    [SerializeField] TMP_Text respawnText;
    float lastDeath;
    bool respawning;

    void OnEnable()
    {
        PlayerManager.OnLocalPlayerDeath += OnLocalPlayerDeath;
        PlayerManager.OnLocalPlayerSpawn += OnLocalPlayerSpawn;

        target.enabled = false;
    }

    void OnDisable()
    {
        PlayerManager.OnLocalPlayerDeath -= OnLocalPlayerDeath;
        PlayerManager.OnLocalPlayerSpawn -= OnLocalPlayerSpawn;
    }

    void Update ()
    {
        if (!target.enabled)
            return;

        UpdateRespawnText();
    }

    void OnLocalPlayerDeath(Vector3 position)
    {
        target.enabled = true;

        lastDeath = Time.time;
        respawning = !PlayerManager.LocalPlayerManager.IsRespawnLocked();

        UpdateRespawnText();
    }

    void OnLocalPlayerSpawn(GameObject playerObject, Vector3 position)
    {
        target.enabled = false;
    }

    void UpdateRespawnText()
    {
        float respawnTime = Mathf.Round((lastDeath + PlayerManager.spawnCooldown - Time.time) * 10f) / 10f;
        respawnText.text = respawning ? $"Respawn in {respawnTime}s" : "You exploded! No respawning now.";
    }

    public void OnLeaveRoomClicked ()
    {
        PhotonNetwork.LeaveRoom();
    }
}
