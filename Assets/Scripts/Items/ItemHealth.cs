using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : MonoBehaviour, IItem
{
    public float health = 20;

    public void Use(GameObject target)
    {
        LivingEntity life = target.GetComponent<LivingEntity>();

        if(life != null)
        {
            life.Regeneration(health);
        }

        Destroy(gameObject);
    }
}
