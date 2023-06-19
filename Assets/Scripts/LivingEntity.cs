using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool dead;
    public event Action onDeath;
    protected virtual void OnEnable()
    {
        dead = false;
        currentHealth = maxHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void Regeneration(float health)
    {
        if (dead)
        {
            return;
        }
        currentHealth += health;
    }
    public virtual void Die()
    {
        if (onDeath != null)
        {
            onDeath();
        }
        dead = true;
    }
}
