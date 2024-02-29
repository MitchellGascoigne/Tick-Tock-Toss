using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] ItemInfo targetItem;
    [SerializeField] bool refillAllItems;
    [SerializeField] int ammoAmount;
    
    public int Pickup (out bool o_refillAllItems, out ItemInfo item)
    {
        // Doing "o_refillAllItems" is just to differentiate the variable in this class from the one in this function. It's not standard practice to do that, mind you.
        o_refillAllItems = refillAllItems;
        item = targetItem;
        return Pickup();
    }

    public int Pickup ()
    {
        return ammoAmount;
    }
}
