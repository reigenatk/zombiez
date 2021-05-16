using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Scope : MonoBehaviour
{
    public Animator animator;

    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public Camera mainCamera;

    [SerializeField] float scopeDelay = 0.15f;
    [SerializeField] float scopedFOV = 15f;
    [SerializeField] FirstPersonController controller;
    [SerializeField] float zoomInSens = 0.3f;
    [SerializeField] float zoomOutSens = 2f;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] float reloadSoundPause = 0.4f;
    
    private float previousFOV = 60f;

    private bool isScoped = false;

    private void Start()
    {
        scopeOverlay.SetActive(isScoped);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // if right mouse button is down
            // if true, set false. if false set true
            isScoped = !isScoped;
            animator.SetBool("IsScoped", isScoped);
          

            
            if (isScoped)
            {
                StartCoroutine(OnScoped());
            } else
            {
                OnUnscoped();
            }
        }
    }
    
    void OnUnscoped()
    {
        // disable the scope image
        scopeOverlay.SetActive(false);

        // show the weapon
        weaponCamera.SetActive(true);

        ZoomOutSens();
        mainCamera.fieldOfView = previousFOV;
    }

    IEnumerator OnScoped()
    {
        // this will disable the weapon and open up the scope
        yield return new WaitForSeconds(scopeDelay);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        ZoomInSens();
        previousFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }
    public void UnscopeAfterShot()
    {
        isScoped = false;
        animator.SetBool("IsScoped", false);
        OnUnscoped();
    }

    public void PlayReloadSound()
    {
        StartCoroutine(PlayReloadSoundHelper());
    }

    private IEnumerator PlayReloadSoundHelper()
    {
        // wait a little bit to play the reload sound
        yield return new WaitForSeconds(reloadSoundPause);
        GetComponent<AudioSource>().PlayOneShot(reloadSound);
    }

    private void ZoomOutSens()
    {
        controller.m_MouseLook.XSensitivity = zoomOutSens;
        controller.m_MouseLook.YSensitivity = zoomOutSens;
    }

    private void ZoomInSens()
    {
        controller.m_MouseLook.XSensitivity = zoomInSens;
        controller.m_MouseLook.YSensitivity = zoomInSens;
    }

}
