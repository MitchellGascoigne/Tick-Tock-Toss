using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] bool impulse;
    [SerializeField] Vector3 force;

    void OnTriggerStay (Collider other)
    {
        if (impulse)
            return;

        ApplyForce(other, Time.fixedDeltaTime);
    }

    void OnTriggerEnter (Collider other)
    {
        if (!impulse)
            return;

        ApplyForce(other, 1);
    }

    void ApplyForce (Collider other, float forceMult)
    {
        if (!other.attachedRigidbody)
            return;

        other.attachedRigidbody.AddForce(force * forceMult, ForceMode.Impulse);
    }
}
