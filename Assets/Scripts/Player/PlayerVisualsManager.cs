using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerVisualsManager : MonoBehaviour
{
    public static event Action<int> OnLocalVisualIndexChanged;
    public static PlayerVisualsList VisualsList { get; private set; }
    [SerializeField] PlayerVisualsList visualsList;

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
        visualIndex = Mathf.Clamp(visualIndex, 0, VisualsList.visuals.Length - 1);

        Hashtable hash = new Hashtable();
        hash.Add("vInd", visualIndex);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        OnLocalVisualIndexChanged?.Invoke(visualIndex);
    }

    public static GameObject GetPlayerVisual (Player player)
    {
        return GetVisual(GetPlayerVisualIndex(player));
    }

    public static int GetPlayerVisualIndex (Player player)
    {
        if (player.CustomProperties.ContainsKey("vInd"))
        {
            return Mathf.Clamp((int)player.CustomProperties["vInd"], 0, VisualsList.visuals.Length - 1);
        }

        // Fallback to returning 0
        return 0;
    }
}
