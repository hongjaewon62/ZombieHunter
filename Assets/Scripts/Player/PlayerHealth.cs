using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider healthBar;

    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip itemPickupClip;

    private AudioSource audioSource;
    private Animator animator;

    private PlayerMovement playerMovement;
    private Shooter shooter;

    private void Awake()
    {
        // 사용할 컴포넌트를 가져오기
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        shooter = GetComponent<Shooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        healthBar.gameObject.SetActive(true);
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        playerMovement.enabled = true;
        //shooter.enabled = true;
    }

    public override void Regeneration(float health)
    {
        base.Regeneration(health);

        healthBar.value = currentHealth;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if(!dead)
        {
            audioSource.PlayOneShot(hitClip);
        }

        base.OnDamage(damage, hitPoint, hitDirection);

        healthBar.value = currentHealth;
    }

    public override void Die()
    {
        base.Die();

        audioSource.PlayOneShot(deathClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            IItem item = other.GetComponent<IItem>();

            if(item != null)
            {
                item.Use(gameObject);
                audioSource.PlayOneShot(itemPickupClip);
            }
        }
    }
}


