using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFriction : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float friction;
    float frictionMod;

    void Awake ()
    {
        frictionMod = 1;
    }

    public void Tick ()
    {
        float totalFrction = friction * frictionMod;
        float frictionCoeff = (1 + totalFrction / (1 / Time.fixedDeltaTime));
        rb.velocity = new Vector3(rb.velocity.x / frictionCoeff, rb.velocity.y, rb.velocity.z / frictionCoeff);
    }

    public void MultiplyFrictionMod (float multiplier)
    {
        frictionMod *= multiplier;
    }
}
