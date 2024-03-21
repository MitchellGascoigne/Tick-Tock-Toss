using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Sprite emptySprite;
    [SerializeField] TMP_Text ammoText;

    Item currentItem;
    IAmmo currentAmmoInterface;

    int ammoCount;

    void OnEnable ()
    {
        PlayerItems.OnLocalItemSwitched += OnLocalItemSwitched;

        AssignAmmoEvents();
    }

    void OnDisable ()
    {
        PlayerItems.OnLocalItemSwitched -= OnLocalItemSwitched;

        DeassignAmmoEvents();
    }

    void AssignAmmoEvents ()
    {
        if (currentAmmoInterface == null)
            return;

        currentAmmoInterface.OnAmmoChanged += OnAmmoChanged;
    }

    void DeassignAmmoEvents ()
    {
        if (currentAmmoInterface == null)
            return;

        currentAmmoInterface.OnAmmoChanged -= OnAmmoChanged;
    }

    void OnLocalItemSwitched (Item item)
    {
        // De-assign events from any existing IAmmo interface to prevent subscribing to multiple events.
        DeassignAmmoEvents();

        currentItem = item;
        currentAmmoInterface = item?.GetComponent<IAmmo>();

        AssignAmmoEvents();

        if (currentAmmoInterface != null)
        {
            ammoCount = currentAmmoInterface.GetCurrentAmmo();
        }

        UpdateUI();
    }

    void OnAmmoChanged (int ammoAmount)
    {
        ammoCount = ammoAmount;

        UpdateUI();
    }

    void UpdateUI ()
    {
        // This line effectively shortens an if statement into a single line; if currentItem exists '?', use the itemIcon. If current item doesn't exist ':' use the emptySprite.
        // For more info, look up "ternary operator C#"
        // Also, try not to use more than one ternary operator per line, otherwise your code can look very messy.
        itemImage.sprite = currentItem ? currentItem.GetItem().itemIcon : emptySprite;

        ammoText.text = currentItem ? "x" + ammoCount : "";
    }
}
