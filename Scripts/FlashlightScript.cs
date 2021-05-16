using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.05f;
    [SerializeField] float angleDecay = 0.05f;
    [SerializeField] float rangeDecay = 0.05f;
    [SerializeField] float minimalAngle = 40f;
    [SerializeField] float minimalRange = 10f;
    [SerializeField] float maximalIntensity = 10f;
    [SerializeField] float maximalAngle = 47f;
    [SerializeField] float maximalRange = 50f;
    [SerializeField] TextMeshProUGUI batteryDisplay;

    Light light;

    private void Start()
    {
        light = GetComponent<Light>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // turn off flashlight
            light.enabled = !light.enabled;
        }
        if (light.enabled)
        {
            DecreaseLightAngle();
            DecreaseLightIntensity();
            DecreaseRange();
        }
        
        int ret = calculateStrength();
        batteryDisplay.text = ret.ToString() + "%";
    }

    private int calculateStrength()
    {
        float percentage = light.intensity / maximalIntensity;
        return Mathf.FloorToInt(percentage * 100);
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        light.spotAngle += restoreAngle;
        if (light.spotAngle > maximalAngle)
        {
            light.spotAngle = maximalAngle;
        }
    }

    public void RestoreLightIntensity(float intensityAmount)
    {
        light.intensity += intensityAmount;
        if (light.intensity > maximalIntensity)
        {
            light.intensity = maximalIntensity;
        }
    }

    public void RestoreRange(float rangeAmount)
    {
        light.range += rangeAmount;
        if (light.range > maximalRange)
        {
            light.range = maximalRange;
        }
    }
    private void DecreaseLightIntensity()
    {
        if (light.intensity >= 0)
        {
            light.intensity -= lightDecay * Time.deltaTime;
        }
    }

    private void DecreaseLightAngle()
    {
        if (light.spotAngle >= 0)
        {
            light.spotAngle -= angleDecay * Time.deltaTime;
        }
    }
    private void DecreaseRange()
    {
        if (light.range >= minimalRange)
        {
            light.range -= rangeDecay * Time.deltaTime;
        }
    }
}
