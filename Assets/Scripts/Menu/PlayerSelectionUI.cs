using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSelectionUI : MonoBehaviour
{
    [SerializeField] Transform visualHolder;

    void OnEnable ()
    {
        PlayerVisualsManager.OnLocalVisualIndexChanged += OnLocalVisualIndexChanged;

        OnLocalVisualIndexChanged(PlayerVisualsManager.GetPlayerVisualIndex(PhotonNetwork.LocalPlayer));
    }

    void OnDisable ()
    {
        PlayerVisualsManager.OnLocalVisualIndexChanged -= OnLocalVisualIndexChanged;
    }

    void OnLocalVisualIndexChanged (int newIndex)
    {
        if (visualHolder.childCount > 0)
        {
            Destroy(visualHolder.GetChild(0).gameObject);
        }

        Instantiate(PlayerVisualsManager.GetVisual(newIndex), visualHolder);
    }

    public void PreviousVisual ()
    {
        int curIndex = PlayerVisualsManager.GetPlayerVisualIndex(PhotonNetwork.LocalPlayer);
        int visualLength = PlayerVisualsManager.VisualsList.visuals.Length;

        if (visualLength == 1)
            return;

        int index = Mathf.RoundToInt(Mathf.Repeat(curIndex - 1, visualLength));

        PlayerVisualsManager.SetPlayerVisual(index);
    }

    public void NextVisual ()
    {
        int curIndex = PlayerVisualsManager.GetPlayerVisualIndex(PhotonNetwork.LocalPlayer);
        int visualLength = PlayerVisualsManager.VisualsList.visuals.Length;

        if (visualLength == 1)
            return;

        int index = Mathf.RoundToInt(Mathf.Repeat(curIndex + 1, visualLength));

        PlayerVisualsManager.SetPlayerVisual(index);
    }
}
