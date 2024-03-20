using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This is an interface: A generic set of functions that can be implemented into classes.
// Generally, it is used to allow scripts to communicate without knowing the exact details of another.
// For example, scripts can use GetComponent to get this from a script on an object-
// -and they can add ammo to it without having to know any details (such as the class) of the script.
public interface IAmmo
{
    event Action<int> OnAmmoChanged;
    public void ChangeCurrentAmmo (int ammoAmount);
    public int GetCurrentAmmo ();
    public void ChangeReserveAmmo (int ammoAmount);
    public int GetReserveAmmo ();
}
