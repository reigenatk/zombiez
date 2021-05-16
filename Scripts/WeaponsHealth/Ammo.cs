using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable] // show the contents of this class in inspector
    private class AmmoSlot 
    {
        public AmmoType ammoType;
        public int ammoAmount;

    }

    public int getAmmoCount(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void shootGunOnce(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void addAmmo(int amount)
    {
        for (int i = 0; i < 3; i++)
        {
            if (ammoSlots[i].ammoAmount == -1)
            {
                // don't add ammo to guns that haven't been found
                continue;
            }
            // don't add grenades..
            ammoSlots[i].ammoAmount += amount;
        }
    }

    public void addAmmoForSpecificType(int amount, int slotNumber)
    {
        AmmoSlot a = ammoSlots[slotNumber];
        a.ammoAmount += amount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType a)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == a)
            {
                return slot;
            }
        }
        return null;
    }
}


