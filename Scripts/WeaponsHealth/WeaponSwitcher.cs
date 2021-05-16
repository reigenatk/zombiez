using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetWeaponActive();
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;
        // basically we set only one weapon to active at a time
        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon && weapon.gameObject.GetComponent<Weapon>().hasBeenPickedUp) 
            {
                weapon.gameObject.SetActive(true);
                weapon.gameObject.GetComponent<Weapon>().PlaySwitchNoise();
            } else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;
        ProcessKeyInput();
        ProcessScrollWheel();

        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    public void switchTo(int weaponindx)
    {
        // for when player picks up a new weapon, we wanna switch to it
        currentWeapon = weaponindx;
        SetWeaponActive();
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0; // wrap back around if on last weapon
            } else
            {
                currentWeapon = currentWeapon + 1;
            }
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) { 
            if (currentWeapon == 0)
            {
                currentWeapon = transform.childCount - 1; // wrap back around if on last weapon
            }
            else
            {
                currentWeapon = currentWeapon - 1;
            }
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }
}
