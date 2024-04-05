using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerVisualHandler : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] PhotonView photonView;
    [SerializeField] GameObject previewVisuals; // Visuals that are there so the prefab can be seen in Prefab Mode.
    [SerializeField] Transform visualHolder;

    void Awake ()
    {
        // Visual prefabs require an Animator and PhotonAnimatorView Component.

        GameObject newVisual = Instantiate(PlayerVisualsManager.GetPlayerVisual(photonView.Controller), visualHolder);

        // Set the controller's animator to the one on the new visual
        controller.SetAnimator(newVisual.GetComponent<Animator>());
        // Manually add the PhotonAnimatorView to the list of observed components
        photonView.ObservedComponents.Add(newVisual.GetComponent<PhotonAnimatorView>());
        // Hide the previewVisuals
        previewVisuals.SetActive(false);
    }
}
