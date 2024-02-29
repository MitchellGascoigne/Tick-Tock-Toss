using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoReadout : MonoBehaviour
{
    [SerializeField] TMP_Text readoutText;
    [SerializeField] GameObject targetObject;
    IAmmo ammoInterface;

    void Start ()
    {
        ammoInterface = targetObject.GetComponent<IAmmo>();
    }

    // Not the best way to do this. However, the better ways can take some effort, so this will be fine here.
    void Update ()
    {
        readoutText.text = "AMMO | " + ammoInterface.GetCurrentAmmo();
    }
}
