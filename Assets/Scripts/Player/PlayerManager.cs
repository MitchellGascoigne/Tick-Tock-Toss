using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    const float spawnCooldown = 5f;
    public static event Action<GameObject, Vector3> OnLocalPlayerSpawn;
    public static event Action<Player, GameObject, Vector3> OnPlayerSpawn;
    public static event Action<Vector3> OnLocalPlayerDeath; // Vector3 = death position
    public static event Action<Player, Vector3> OnPlayerDeath;
    public static PlayerManager LocalPlayerManager { get; private set; }

    [SerializeField] GameObject[] spawnPositionObjects;
    PhotonView PV;           // Reference to the PhotonView component attached to this GameObject.
    PlayerController currentPlayer;

    float lastDeath;
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
        if (PV.IsMine)  // If this GameObject belongs to the local player...
        {
            SpawnPlayer();  // ...create the player's controller.
        }
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
        if (respawnLocked)
            return;
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
        if (spawnPositionObjects.Length == 0)
        {
            Debug.LogError("No spawn positions available.");
            return null;
        }

        int randomIndex = Random.Range(0, spawnPositionObjects.Length);
        return spawnPositionObjects[randomIndex].transform;
    }

    bool IsSpawnCooldownOver ()
    {
        return Time.time > lastDeath + spawnCooldown;
    }

    #endregion

    #region Player Death

    void OnDeath (GameObject playerObject, Player player)
    {
        OnPlayerDeath?.Invoke(player, playerObject.transform.position); // Whether or not this is the local player, send the event that a player has died;

        if (player != PV.Owner)
            return;

        OnLocalPlayerDeath?.Invoke(playerObject.transform.position);

        lastDeath = Time.time;
    }

    #endregion
}
