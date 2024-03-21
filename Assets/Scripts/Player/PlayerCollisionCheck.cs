using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollisionCheck : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    [SerializeField] LayerMask ignoreLayers;
    Vector3 lastPosition;

    void Awake ()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }

    void Start ()
    {
        lastPosition = transform.position;
    }

    void FixedUpdate ()
    {
        if (!photonView.IsMine)
            return;

        Vector3 currentPosition = transform.position;

        // If the player has moved enough in one physics frame, assume they are teleporting and don't do the collision check.
        if (Vector3.Distance(lastPosition, currentPosition) > 5f)
        {
            lastPosition = transform.position;
            return;
        }

        Vector3 direction = currentPosition - lastPosition;
        direction.y = 0;
        
        if (Physics.Raycast(lastPosition, direction, out RaycastHit hit, direction.magnitude, ~ignoreLayers, QueryTriggerInteraction.Ignore))
        {
            transform.position = lastPosition;
        }

        lastPosition = transform.position;
    }
}
