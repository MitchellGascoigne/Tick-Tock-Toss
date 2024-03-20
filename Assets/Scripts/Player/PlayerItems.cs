using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerItems : MonoBehaviour
{
    public static event Action<Item> OnLocalItemSwitched;

    PhotonView photonView;
    [SerializeField] Item[] items;
    [SerializeField] bool inputEnabled;


    int itemIndex;
    int previousItemIndex = -1;

    void Start ()
    {
        EquipItem(0);
    }

    void Update ()
    {
        TakeInput();
    }

    void TakeInput ()
    {
        if (!inputEnabled)
            return;
        if (!photonView.IsMine)
            return;

        for (int i = 0; i < items.Length; i++)
        {

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
    }

    public void EquipItem (Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
            {
                EquipItem(item);
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
        items[itemIndex].itemGameObject.SetActive(true);

        if (previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }

        OnLocalItemSwitched?.Invoke(items[itemIndex]);

        previousItemIndex = itemIndex;
    }

    public Item[] GetItems ()
    {
        return items;
    }
}
