using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/GunData", fileName = "GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 1;
    public float fireDistance = 50f;

    public int startAmmo = 100;
    public int magazine = 10;

    public float fireTime = 0.2f;
    public float reloadTime = 2.0f;
}
