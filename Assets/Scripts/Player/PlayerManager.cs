using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using Random = UnityEngine.Random;
using Unity.Jobs;
using System.Collections.Generic;
using Unity.Collections;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    public struct Data
    {

        public int PlayerID;            // Unique identifier for the player
        public bool IsAlive;            // Indicates whether the player is alive
        public Vector3 SpawnPosition;   // The position where the player should spawn
    }

    public const float spawnCooldown = 5f;
    public const float leaveDelay = 3f;
    public static event Action<GameObject, Vector3> OnLocalPlayerSpawn;
    public static event Action<Player, GameObject, Vector3> OnPlayerSpawn;
    public static event Action<Vector3> OnLocalPlayerDeath; // Vector3 = death position
    public static event Action<Player, Vector3> OnPlayerDeath;
    public static PlayerManager LocalPlayerManager { get; private set; }

    [SerializeField] Transform[] spawnPositionObjects; // Transform vs GameObject
    PhotonView PV;           // Reference to the PhotonView component attached to this GameObject.
    PlayerController currentPlayer;

    float lastDeath = -1000f; // Set to a low value to ensure spawning won't be immediately locked from the get-go in any scenario.
    bool respawnLocked;

    #region Initialisation

    private void Awake()
    {
        PV = GetComponent<PhotonView>();  // Get the PhotonView component on this GameObject.

        if (!PV.IsMine)
            return;
        if (LocalPlayerManager)
        {
            Debug.LogWarning($"There are two {nameof(PlayerManager)}s in existence. Destroying this {nameof(PlayerManager)}");
            Destroy(gameObject);
            return;
        }

        LocalPlayerManager = this;
    }

    public override void OnEnable ()
    {
        base.OnEnable(); // Not necessary here, but i find it good practice to almost always call the base function.

        PlayerController.OnDeath += OnDeath;
        PlayerController.OnSpawn += OnSpawn;
    }

    public override void OnDisable ()
    {
        base.OnDisable();

        PlayerController.OnDeath -= OnDeath;
        PlayerController.OnSpawn -= OnSpawn;
    }

    private void Start()
    {
        SpawnPlayer(); // No checks are done, as SpawnPlayer already includes all spawning checks.
    }

    #endregion

    #region Player Spawning

    // Useful for when the player detonates and you don't want them to respawn anymore.
    public void SetRespawnLockState(bool _respawnLocked)
    {
        respawnLocked = _respawnLocked;
    }

    void OnSpawn (GameObject playerObject, Player player)
    {
        if (this != LocalPlayerManager) // Only execute this code for the LocalPlayerManager, otherwise Actions will trigger twice.
            return;

        OnPlayerSpawn?.Invoke(player, playerObject, playerObject.transform.position); // Whether or not this is the local player, send the event that a player has spawned.

        if (player != PV.Owner)
            return;

        OnLocalPlayerSpawn?.Invoke(playerObject, playerObject.transform.position);
    }

    void FixedUpdate ()
    {
        // Normally, I wouldn't do spawning logic in a constantly-running function, but honestly - this way is easier and shouldn't cause any issues.
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (!PV.IsMine)
            return;
        if (respawnLocked)
        {
            Invoke(nameof(LeaveRoom), leaveDelay);
            return;
        }
        if (currentPlayer)
            return;
        if (!IsSpawnCooldownOver())
            return;

        Debug.Log("Instantiated Player Controller");

        // Call SpawnPosition to select a random spawn position.
        Transform selectedSpawnPosition = SpawnPosition();

        // Instantiate the player's controller GameObject for the local player.
        // The "PhotonPrefabs" folder is used to locate the player prefab.
        // It's placed at the selected spawn position with no rotation.
        GameObject playerController = PhotonNetwork.Instantiate("PlayerController", selectedSpawnPosition.position, Quaternion.identity);
        currentPlayer = playerController.GetComponent<PlayerController>();
    }

    public Transform SpawnPosition()
    {
        return SpawnManager.Instance.GetSpawnPoint();
    }

    bool IsSpawnCooldownOver ()
    {
        return Time.time > lastDeath + spawnCooldown;
    }

    public bool IsRespawnLocked ()
    {
        return respawnLocked;
    }

    #endregion

    #region Player Death

    void OnDeath (GameObject playerObject, Player player)
    {
        if (this != LocalPlayerManager) // Only execute this code for the LocalPlayerManager, otherwise Actions will trigger twice.
            return;

        OnPlayerDeath?.Invoke(player, playerObject.transform.position); // Whether or not this is the local player, send the event that a player has died.

        if (player != PhotonNetwork.LocalPlayer)
            return;

        OnLocalPlayerDeath?.Invoke(playerObject.transform.position);

        lastDeath = Time.time;
    }

    #endregion

    void LeaveRoom ()
    {
        PhotonNetwork.LeaveRoom();
    }
}
