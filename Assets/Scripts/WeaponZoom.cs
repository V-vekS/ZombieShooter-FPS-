using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using FirstPersonController = UnityStandardAssets.Characters.FirstPerson.FirstPersonController;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] FirstPersonController fpController;    

    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 30f;

    

    [SerializeField] float zoomedInSensitivity = 1f;
    [SerializeField] float zoomedOutSensitivity = 2f;

    bool zoomedInToggle = false;

    private void OnDisable()
    {
        ZoomOut();
    }
   
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(zoomedInToggle ==false)
            {
                ZoomIn();

            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void ZoomOut()
    {
        zoomedInToggle = false;
        fpsCamera.fieldOfView = zoomedOutFOV;
        fpController.m_MouseLook.XSensitivity = zoomedOutSensitivity;
        fpController.m_MouseLook.YSensitivity = zoomedOutSensitivity;
    }

    private void ZoomIn()
    {
        zoomedInToggle = true;
        fpsCamera.fieldOfView = zoomedInFOV;
        fpController.m_MouseLook.XSensitivity = zoomedInSensitivity;
        fpController.m_MouseLook.YSensitivity = zoomedInSensitivity;
    }
}
