using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveThrower : MonoBehaviour
{
    public float throwforce = 40f;
    public GameObject grenadePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowGrenade();
        }
        
    }
    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwforce, ForceMode.VelocityChange);
    }
}
// https://www.youtube.com/watch?v=BYL6JtUdEY0