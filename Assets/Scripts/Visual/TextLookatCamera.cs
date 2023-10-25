using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class TextLookatCamera : MonoBehaviour
{
    void Update ()
    {
        // Set forward facing away from the camera, since the text is flipped
        transform.forward = -(Camera.current.transform.position - transform.position); 
    }
}
