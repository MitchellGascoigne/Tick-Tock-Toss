using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHold : MonoBehaviour
{

    public GameObject Object;
    public Transform PlayerTransform;
    public float range = 3f;
    public float Go = 100f;
    public Camera Camera;

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StartPickUp();
        }
        if (Input.GetKeyUp("f"))
        {
            Drop();
        }
    }
    void StartPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                Pickup();
            }
        }
    }
        void Pickup()
        {
            Object.transform.SetParent(PlayerTransform);
        }

        void Drop()
        {
            PlayerTransform.DetachChildren();
        }
   

}
