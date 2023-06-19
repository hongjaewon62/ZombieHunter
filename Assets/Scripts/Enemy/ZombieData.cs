using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/ZombieData", fileName ="ZombieData")]
public class ZombieData : ScriptableObject
{
    public float health = 100f;
    public float damage = 10f;
    public float attackCooldown = 0.5f;
    public float speed = 2f;

    public AudioClip deathSound;
    public AudioClip hitSound;
}
