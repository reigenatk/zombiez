using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    
    [SerializeField] Camera playerCam;
    [SerializeField] float zoomOut;
    [SerializeField] float zoomIn;
    bool zoomedIn = false;
    [SerializeField] FirstPersonController controller;
    [SerializeField] float zoomInSens = 2.5f;
    [SerializeField] float zoomOutSens = 2f;

    // weapon will be disabled after we switch out of it
    // when it is swapped, make sure to zoom out. The controller is not disabled
    // since it sits on player.
    private void OnDisable()
    {
        ZoomOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && zoomedIn)
        {
            ZoomOut();
        }
        else if (Input.GetMouseButtonDown(1) && !zoomedIn)
        {
            ZoomIn();
        }
    }

    private void ZoomOut()
    {
        playerCam.fieldOfView = zoomOut;
        zoomedIn = false;
        controller.m_MouseLook.XSensitivity = zoomOutSens;
        controller.m_MouseLook.YSensitivity = zoomOutSens;
    }

    private void ZoomIn()
    {
        playerCam.fieldOfView = zoomIn;
        zoomedIn = true;
        controller.m_MouseLook.XSensitivity = zoomInSens;
        controller.m_MouseLook.YSensitivity = zoomInSens;
    }
}
