using UnityEngine;

[CreateAssetMenu(menuName = "Game/Controls/Control Parameters")]
public class ControlParameters : ScriptableObject
{
    public float lookSensitivity = 1;
    public bool invertX;
    public bool invertY;

    public float zoomSensitivity = 0.25f;
    public float zoomSmoothing = 0.5f;
    public float minCameraDistance = 1.5f;
    public float maxCameraDistance = 10f;
}
