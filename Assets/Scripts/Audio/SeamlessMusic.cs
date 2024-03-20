using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamlessMusic : MonoBehaviour
{
    [SerializeField] AudioSource target;
    static float audioTimestamp;

    // When loading a scene, set the AudioSource's time.
    void Start ()
    {
        Debug.Log(audioTimestamp);
        target.time = audioTimestamp;
    }

    // When unloading a scene, set the timestamp to the Audiosource's current time.
    void OnDisable ()
    {
        audioTimestamp = target.time;
        Debug.Log(target.time + "d");
    }
}
