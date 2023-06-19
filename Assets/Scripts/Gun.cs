using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    public State state { get; private set; }

    public Transform fireTransform;

    private AudioSource audioSource;

    public GunData gunData;

    public Camera cam;

    public int currentAmmo = 100;
    public int currentMagazine;

    private float lastFireTime;

    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = gunData.startAmmo;
        currentMagazine = gunData.magazine;
    }

    private void OnEnable()
    {
        state = State.Ready;
        lastFireTime = 0;
    }

    public void Fire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + gunData.fireTime)
        {
            if(currentAmmo != 0 || currentMagazine != 0)
            {
                lastFireTime = Time.time;
                Shot();
            }
        }
    }

    private void Shot()
    {
        muzzleFlash.Play();
        audioSource.PlayOneShot(gunData.shotClip);
        Vector3 hitPosition = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, gunData.fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            GameObject hitEffectObj = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitEffectObj, 1f);
            if (target != null)
            {
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = cam.transform.position + cam.transform.forward * gunData.fireDistance;
        }
        currentMagazine--;
        if (currentMagazine <= 0)
        {
            Reload();
        }
    }

    public bool Reload()
    {
        if(state == State.Reloading || currentAmmo <= 0 || currentMagazine >= gunData.magazine)
        {
            return false;
        }
        StartCoroutine(ReloadCoroutine());
        return true;
    }

    private IEnumerator ReloadCoroutine()
    {
        state = State.Reloading;
        audioSource.PlayOneShot(gunData.reloadClip);
        yield return new WaitForSeconds(gunData.reloadTime);

        int ammoToFill = gunData.magazine - currentMagazine;
        if(currentAmmo < ammoToFill)
        {
            ammoToFill = currentAmmo;
        }

        currentMagazine += ammoToFill;
        currentAmmo -= ammoToFill;
        state = State.Ready;
    }

    public void Restart()
    {
        currentAmmo = gunData.startAmmo;
        currentMagazine = gunData.magazine;

        state = State.Ready;
        lastFireTime = 0;
    }
}
