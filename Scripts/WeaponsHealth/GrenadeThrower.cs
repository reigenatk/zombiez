using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject grenadePrefab;
    [SerializeField] AmmoType ammoType;
    public Ammo ammoSlot;
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip grenadeThrowSound;
    [SerializeField] AudioClip grenadePickupSound;
    [SerializeField] TextMeshProUGUI ammoText;
    public bool hasPickedUpGrenade = false;

    // Update is called once per frame
    void Update()
    {
        DisplayGrenadeAmmo();
        if (Input.GetKeyDown(KeyCode.G) && hasPickedUpGrenade)
        {
            // press F to throw grenade
            if (ammoSlot.getAmmoCount(ammoType) > 0)
            {
                ThrowGrenade();
                ammoSlot.shootGunOnce(ammoType); // decrease ammo by 1
                playGrenadeThrowNoise();
            }
        }
    }
    public void playGrenadeThrowNoise()
    {
        audioPlayer.PlayOneShot(grenadeThrowSound);
    }

    public void playGrenadePickupNoise()
    {
        audioPlayer.PlayOneShot(grenadePickupSound);
    }

    private void DisplayGrenadeAmmo()
    {
        int currentAmmo = ammoSlot.getAmmoCount(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
