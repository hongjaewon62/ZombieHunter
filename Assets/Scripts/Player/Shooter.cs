using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Gun gun;
    public Transform gunPivot;
    public Transform leftHandMount;
    public Transform rightHandMount;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();   
    }

    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.instance != null && !GameManager.instance.isGameover)
        {
            if (Input.GetButton("Fire1"))
            {
                gun.Fire();
            }
            else if (Input.GetKey(KeyCode.R))
            {
                gun.Reload();
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (gun != null && UiManager.instance != null)
        {
            UiManager.instance.AmmoText(gun.currentMagazine, gun.currentAmmo);
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }

}
