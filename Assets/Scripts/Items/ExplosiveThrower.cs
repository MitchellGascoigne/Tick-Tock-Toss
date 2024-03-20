using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveThrower : Item, IAmmo
{
    public event Action<int> OnAmmoChanged;

    ExplosiveThrowerInfo explosiveThrowerInfo;
    int ammo;

    protected override void Initialise ()
    {
        base.Initialise();

        explosiveThrowerInfo = itemInfo as ExplosiveThrowerInfo;
    }

    public override void Use ()
    {
        // Not necessary. Good practice to do for this item system, in my opinion, but it's up to your preference.
        base.Use();

        ThrowGrenade();
    }

    void ThrowGrenade()
    {
        if (ammo <= 0)
            return;

        GameObject grenade = Instantiate(explosiveThrowerInfo.explosivePrefab, itemGameObject.transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * explosiveThrowerInfo.throwForce, ForceMode.VelocityChange);

        ChangeCurrentAmmo(-1);
    }

    public void ChangeCurrentAmmo (int ammoAmount)
    {
        ammo += ammoAmount;

        if (ammo < 0)
        {
            ammo = 0;
        }
        // This is so that the ammo value clamping is not done if maxAmmo is less than -1, in case infinite max ammo is wanted.
        if (explosiveThrowerInfo.maxAmmo > -1 && ammo > explosiveThrowerInfo.maxAmmo)
        {
            ammo = explosiveThrowerInfo.maxAmmo;
        }

        OnAmmoChanged?.Invoke(ammo);
    }

    public int GetCurrentAmmo ()
    {
        return ammo;
    }

    public void ChangeReserveAmmo (int ammoAmount)
    {
        // Use ChangeCurrentAmmo, as we do not have reserve ammo for explosives.
        ChangeCurrentAmmo(ammoAmount);
    }

    public int GetReserveAmmo ()
    {
        // Use GetCurrentAmmo, as we do not have reserve ammo for explosives.
        return GetCurrentAmmo();
    }
}
// https://www.youtube.com/watch?v=BYL6JtUdEY0