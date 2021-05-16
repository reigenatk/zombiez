using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            
            Destroy(gameObject);
            Debug.Log("replaced");
        }
    }
}
