using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerItems : MonoBehaviour
{
    public static event Action<Item> OnLocalItemSwitched;

    [SerializeField] PhotonView photonView;
    [SerializeField] Item[] items;
    [SerializeField] bool manualSwitchEnabled;

    int itemIndex;
    int previousItemIndex = -2;

    IAmmo currentItemAmmo;

    void Awake ()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }

    void Start ()
    {
        EquipItem(-1);
    }

    void Update ()
    {
        TakeInput();
    }

    void TakeInput ()
    {
        if (!photonView.IsMine)
            return;
        if (itemIndex <= -1)
            return;

        for (int i = 0; i < items.Length; i++)
        {
            if (!manualSwitchEnabled)
                break;
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            items[itemIndex].Use();
        }

        if (currentItemAmmo != null && currentItemAmmo.GetCurrentAmmo() <= 0)
        {
            EquipItem(-1);
        }
    }

    public void EquipItem (Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
            {
                EquipItem(i);
                return;
            }
        }
    }

    void EquipItem (int _index)
    {
        if (!photonView.IsMine)
            return;
        if (_index == previousItemIndex)
            return;

        itemIndex = _index;
        if (itemIndex > -1)
        {
            CurrentItem().itemGameObject.SetActive(true);
            currentItemAmmo = CurrentItem().GetComponent<IAmmo>();
        } else
        {
            currentItemAmmo = null;
        }

        if (previousItemIndex > -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }

        OnLocalItemSwitched?.Invoke(CurrentItem());

        previousItemIndex = itemIndex;
    }

    Item CurrentItem ()
    {
        if (itemIndex > -1)
            return items[itemIndex];

        return null;
    }

    public Item[] GetItems ()
    {
        return items;
    }
}
