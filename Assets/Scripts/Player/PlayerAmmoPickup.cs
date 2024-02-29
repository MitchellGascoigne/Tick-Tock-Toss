using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAmmoPickup : MonoBehaviour
{
    [SerializeField] PhotonView referenceView;
    [SerializeField] PlayerController playerController;

    void Awake ()
    {
        if (!referenceView)
        {
            referenceView = gameObject.GetComponent<PhotonView>();
        }
    }

    void OnTriggerEnter (Collider other)
    {
        // Don't execute the code if this isn't your GameObject.
        if (!referenceView.IsMine)
            return;
        // If the layer isn't correct, stop executing the function.
        if (other.gameObject.layer != LayerMask.NameToLayer(AmmoPickup.layerName))
            return;

        Pickup(other.gameObject);
    }

    // Not the most efficient function. It doesn't matter in this case, as this function won't be called that often.
    void Pickup (GameObject pickupObject)
    {
        AmmoPickup ammoPickup = pickupObject.GetComponent<AmmoPickup>();

        int ammoAmount = ammoPickup.Pickup(out bool refillAllItems, out ItemInfo targetItem);

        foreach (Item item in playerController.GetItems())
        {
            // If refilling all items, then don't bother with the item == targetItem check. Just try to refill this item's ammo, then continue to the next item.
            if (refillAllItems)
            {
                RefillItem(item, ammoAmount);
                continue;
            }

            if (item.GetItem() == targetItem)
            {
                RefillItem(item, ammoAmount);
            }
        }
    }

    void RefillItem (Item item, int ammoAmount)
    {
        IAmmo ammoInterface = item.GetComponent<IAmmo>();
        if (ammoInterface == null)
            return;

        ammoInterface.ChangeReserveAmmo(ammoAmount);
    }
}
