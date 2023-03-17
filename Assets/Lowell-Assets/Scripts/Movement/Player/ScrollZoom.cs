using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollZoom : MonoBehaviour
{
    [SerializeField] ControlParameters controlParams;
    private Cinemachine.Cinemachine3rdPersonFollow virtualCamera;

    private float zoomFac;
    private float targetZoomFac;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>();
        zoomFac = 0.5f;
        targetZoomFac = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        targetZoomFac += InputManager.deltaZoom * controlParams.zoomSensitivity;
        targetZoomFac = Mathf.Clamp01(targetZoomFac);
        zoomFac = Mathf.Lerp(zoomFac, targetZoomFac, DampedFac(controlParams.zoomSmoothing));
        
        virtualCamera.CameraDistance = Mathf.SmoothStep(controlParams.minCameraDistance, controlParams.maxCameraDistance, zoomFac);
    }

    float DampedFac(float smoothing) {
        return 1 - Mathf.Exp(-Time.deltaTime / smoothing);
    }
}
