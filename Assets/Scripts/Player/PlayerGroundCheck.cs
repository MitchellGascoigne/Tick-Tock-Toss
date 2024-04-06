using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public bool Grounded { get { return GetGroundedState(); } }
    bool grounded;
    [SerializeField] Rigidbody rb;
    [SerializeField] SphereCollider checkCollider;
    [SerializeField] LayerMask ignoreLayers;
    bool checkedThisFixedUpdate;

    void FixedUpdate ()
    {
        checkedThisFixedUpdate = false;
    }

    bool GetGroundedState ()
    {
        if (!checkedThisFixedUpdate)
        {
            grounded = (Physics.OverlapSphere(checkCollider.transform.position, checkCollider.radius, ~ignoreLayers, QueryTriggerInteraction.Ignore).Length > 0) ? true : false;
            checkedThisFixedUpdate = true;
        }

        return grounded;
    }

    bool GetProcessedGroundedState ()
    {
        return grounded ? Mathf.Approximately(rb.velocity.y, 0) : false;
    }
}
