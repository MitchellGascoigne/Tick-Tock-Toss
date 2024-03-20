using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Explosive Thrower")]
public class ExplosiveThrowerInfo : ItemInfo
{
    public float throwForce = 10f;
    public GameObject explosivePrefab;
    public int maxAmmo = 1;
}
