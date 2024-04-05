using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerVisualsManager : MonoBehaviour
{
    [SerializeField] PlayerVisualsList visualsList;
    public static PlayerVisualsList VisualsList { get; private set; }

    void Awake ()
    {
        VisualsList = visualsList;
    }

    public static GameObject GetVisual (int visualIndex)
    {
        return VisualsList.visuals[visualIndex];
    }

    public static void SetPlayerVisual (int visualIndex)
    {
        Hashtable hash = new Hashtable();
        hash.Add("vInd", visualIndex);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public static GameObject GetPlayerVisual (Player player)
    {
        return GetVisual(GetPlayerVisualIndex(player));
    }

    public static int GetPlayerVisualIndex (Player player)
    {
        if (player.CustomProperties.ContainsKey("vInd"))
        {
            return (int)player.CustomProperties["vInd"];
        }

        // Fallback to returning 0
        return 0;
    }
}
