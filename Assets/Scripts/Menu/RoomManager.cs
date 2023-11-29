using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if (Instance) // checks if another RoomManager exists
        {
            Destroy(gameObject); // there can only be one
            return;
        }

        DontDestroyOnLoad(gameObject); // I am the only one
        Instance = this;

    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded( Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "Game") // We are in the game scene
        {
            PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        PhotonNetwork.LoadLevel("Menu"); // Go to the menu after leaving the room, so other scripts can just call PhotonNetwork.LeaveRoom() without worrying about loading the menu.
    }
}
