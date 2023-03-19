using UnityEngine;

public class ScrollZoom : MonoBehaviour
{
    ControlParameters controlParams;
    private Cinemachine.Cinemachine3rdPersonFollow virtualCamera;

    private float zoomFac;
    private float targetZoomFac;
    // Start is called before the first frame update
    void Start()
    {

        controlParams = ControlParameters.instance;
        virtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>();
        zoomFac = (virtualCamera.CameraDistance - controlParams.minCameraDistance) / (controlParams.maxCameraDistance - controlParams.minCameraDistance);
        targetZoomFac = zoomFac;
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
