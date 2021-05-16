using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moon : MonoBehaviour
{
    [SerializeField] GameObject sun;
    public Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float deg = sun.transform.eulerAngles.x;
        if (deg >= 0 && deg <= 90) // day, so turn moon off
        {
            // Debug.Log("Moon On");
            light.enabled = false;
        }
        else
        {
            // Debug.Log("Moon Off");
            light.enabled = true;
        }
        transform.LookAt(Vector3.zero);
    }
}
