using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGunSound : MonoBehaviour
{
    [SerializeField] AudioSource gunSound;

    private void Start()
    {
        gunSound.Play();
    }
}
