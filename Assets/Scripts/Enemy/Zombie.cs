using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : LivingEntity
{
    public LayerMask targetLayer;

    private LivingEntity target;
    private NavMeshAgent navMeshAgent;

    public ParticleSystem hitEffect;

    public ZombieData zombieData;

    private Animator animator;
    private AudioSource audioSource;

    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip attackSound;

    private float damage = 10f;
    private float attackCooldown = 2f;
    private float lastAttackTime;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();


    }

    private void Start()
    {
        StartCoroutine(FindTarget());
        Setup();
    }

    public void Setup()
    {
        maxHealth = zombieData.health;
        currentHealth = zombieData.health;
        damage = zombieData.damage;
        attackCooldown = zombieData.attackCooldown;
        navMeshAgent.speed = zombieData.speed;
        deathSound = zombieData.deathSound;
        hitSound = zombieData.hitSound;
    }

    private bool CheckTarget
    {
        get
        {
            if (target != null && !target.dead)
            {
                return true;
            }

            return false;
        }
    }

    private IEnumerator FindTarget()
    {
        while (!dead)
        {
            if(CheckTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(target.transform.position);
                animator.SetBool("Walk", true);
            }
            else
            {
                navMeshAgent.isStopped = true;

                animator.SetBool("Walk", false);
                Collider[] colliders = Physics.OverlapSphere(transform.position, 200f, targetLayer);

                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    if (livingEntity != null && !livingEntity.dead)
                    {
                        target = livingEntity;

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            audioSource.PlayOneShot(hitSound);
        }

        base.OnDamage(damage, hitPoint, hitNormal);
    }
    public override void Die()
    {
        base.Die();

        Collider[] zombieColliders = GetComponents<Collider>();
        for (int i = 0; i< zombieColliders.Length; i++)
        {
            zombieColliders[i].enabled = false;
        }
        animator.SetTrigger("Death");
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        audioSource.PlayOneShot(deathSound);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!dead && Time.time >= lastAttackTime + attackCooldown)
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();

            if (attackTarget != null && attackTarget == target)
            {
                lastAttackTime = Time.time;

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                if(!GameManager.instance.isGameover)
                {
                    StartCoroutine(CoroutineAttack(attackTarget, hitPoint, hitNormal));
                }
            }
        }
    }

    IEnumerator CoroutineAttack(LivingEntity target, Vector3 hitPoint, Vector3 hitNormal)
    {
        animator.SetBool("ChangeWalk", false);
        animator.SetTrigger("Attack");
        target.OnDamage(damage, hitPoint, hitNormal);
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("ChangeWalk", true);
    }
}
