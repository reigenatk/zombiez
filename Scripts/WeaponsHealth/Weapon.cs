using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 100f; // how far the raycast goes
    [SerializeField] float damagePerBullet = 10f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float fireRate = 0.5f;
    public Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] AudioClip switchSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] TextMeshProUGUI ammoText;
    public int weaponIdx;
    public bool hasBeenPickedUp = false;

    private float nextFire = 0f;
    AudioSource audioPlayer;
    public LayerMask ignoreLayers;
    Camera FPSCamera;
    Animation shootAnimation;


    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        FPSCamera = GetComponentInParent<Camera>();
        shootAnimation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
            /*if (animationController)
            {
                animationController.SetTrigger("Shoot");
            }*/
        }
        else
        {
            /*if (animationController)
            {
                animationController.ResetTrigger("Shoot");
            }*/
        }
    }

    private void DisplayAmmo()
    {
        if (ammoSlot == null)
        {
            return;
        }
        int currentAmmo = ammoSlot.getAmmoCount(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    void Shoot()
    {
        if (shootAnimation)
        {
            shootAnimation.Play();
        }
        if (ammoSlot.getAmmoCount(ammoType) > 0)
        {
            ammoSlot.shootGunOnce(ammoType);
            audioPlayer.PlayOneShot(shootSound);
            PlayMuzzleFlash();
            ProcessRaycast();
            if (GetComponent<Scope>() != null)
            {
                // if the weapon we are shooting is the sniper
                // after taking a shot, unscope
                GetComponent<Scope>().UnscopeAfterShot();
                GetComponent<Scope>().PlayReloadSound();
            }
        }
    }

    public void pickedUp()
    {
        hasBeenPickedUp = true;
    }

    public void PlaySwitchNoise()
    {
        audioPlayer.PlayOneShot(switchSound);
    }
    public void PlayPickupNoise()
    {
        AudioSource weaponAudioSource = GetComponentInParent<AudioSource>();
        weaponAudioSource.PlayOneShot(switchSound);
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range, ~ignoreLayers))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return; // if we hit a wall or the ground
            }
            target.takeDamage(damagePerBullet);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        // hit.point is just the location that the raycast hit a collider
        GameObject particleEffect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particleEffect, 0.1f);
    }
}
