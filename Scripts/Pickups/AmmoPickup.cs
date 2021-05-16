using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int amountToAdd = 5;
    private void OnTriggerEnter(Collider other)
    {
        // if the trigger is with a player
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().addAmmo(amountToAdd);
            GetComponentInParent<PickupPlayer>().PlayAmmoPickupNoise(); // play the ammo pickup sound
            Debug.Log("OnTriggerEnter");
            Destroy(gameObject);
        }
    }
}
