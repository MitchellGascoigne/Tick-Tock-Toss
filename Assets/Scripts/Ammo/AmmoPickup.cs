using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AmmoPickup : MonoBehaviour
{
    public const string layerName = "Ammo Pickup";
    [SerializeField] ItemInfo targetItem;
    [SerializeField] bool refillAllItems;
    [SerializeField] int ammoAmount;
    [SerializeField] bool dieOnPickup;

    void Awake ()
    {
        // Ensure that this GameObject has the correct layer.
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    // The "out" keyword means that the variable is being 'given' to another script.
    // This can be a little confusing. If you want to know more, google it or ask whoever is teaching you.
    public int Pickup (out bool o_refillAllItems, out ItemInfo item)
    {
        // Doing "o_refillAllItems" is just to differentiate the variable in this class from the one in this function. It's not standard practice to do that, mind you.
        o_refillAllItems = refillAllItems;
        item = targetItem;
        return Pickup();
    }

    public int Pickup ()
    {
        if (dieOnPickup)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        return ammoAmount;
    }
}
