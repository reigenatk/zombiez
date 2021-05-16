using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{

    [SerializeField] float restoreAngle = 20f;
    [SerializeField] float restoreIntensity = 5f;
    [SerializeField] float restoreRange = 10f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponentInParent<PickupPlayer>().PlayBatteryPickupNoise();
            other.GetComponentInChildren<FlashlightScript>().RestoreLightAngle(restoreAngle);
            other.GetComponentInChildren<FlashlightScript>().RestoreRange(restoreRange);
            other.GetComponentInChildren<FlashlightScript>().RestoreLightIntensity(restoreIntensity);
            Destroy(gameObject);
        }
    }
}
