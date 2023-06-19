using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAmmo : MonoBehaviour, IItem
{
    public int ammo = 30;

    public void Use(GameObject target)
    {
        Shooter[] shooter = target.GetComponents<Shooter>();

        if(shooter != null && shooter[0].gun != null && shooter[0].enabled)
        {
            shooter[0].gun.currentAmmo += ammo;
        }

        if (shooter != null && shooter[1].gun != null && shooter[1].enabled)
        {
            shooter[1].gun.currentAmmo += ammo;
        }

        Destroy(gameObject);
    }
}
