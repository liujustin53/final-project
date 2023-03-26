using UnityEngine;

public class ScrollZoom : MonoBehaviour
{
    private Cinemachine.Cinemachine3rdPersonFollow virtualCamera;

    private float zoomFac;
    private float targetZoomFac;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>();
        zoomFac = (virtualCamera.CameraDistance - ControlParameters.minCameraDistance) / (ControlParameters.maxCameraDistance - ControlParameters.minCameraDistance);
        targetZoomFac = zoomFac;
    }

    // Update is called once per frame
    void Update()
    {
        targetZoomFac += InputManager.deltaZoom * ControlParameters.zoomSensitivity;
        targetZoomFac = Mathf.Clamp01(targetZoomFac);
        zoomFac = Mathf.Lerp(zoomFac, targetZoomFac, DampedFac(ControlParameters.zoomSmoothing));
        
        virtualCamera.CameraDistance = Mathf.SmoothStep(ControlParameters.minCameraDistance, ControlParameters.maxCameraDistance, zoomFac);
    }

    float DampedFac(float smoothing) {
        return 1 - Mathf.Exp(-Time.deltaTime / smoothing);
    }
}
