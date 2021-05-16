using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPlayer : MonoBehaviour
{

    [SerializeField] AudioClip ammoPickupSound;
    [SerializeField] AudioClip batteryPickupSound;
    public void PlayAmmoPickupNoise()
    {
        GetComponent<AudioSource>().PlayOneShot(ammoPickupSound);
    }
    public void PlayBatteryPickupNoise()
    {
        GetComponent<AudioSource>().PlayOneShot(batteryPickupSound);
    }
}
