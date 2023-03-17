using UnityEngine;


/// Globally adjusts player control settings.
[CreateAssetMenu(menuName = "Game/Controls/Control Parameters")]
public class ControlParameters : ScriptableObject
{
    public static ControlParameters instance;
    public float lookSensitivity = 1;
    public bool invertX;
    public bool invertY;

    public float zoomSensitivity = 0.25f;
    public float zoomSmoothing = 0.5f;
    public float minCameraDistance = 1.5f;
    public float maxCameraDistance = 10f;

    // The Player can start jumping for this long after leaving the ground
    public float coyoteTime = 0.2f;

    // The Player will try to jump for this long after first pressing the button
    public float jumpBuffer = 0.2f;

    void Awake() {
        instance = this;
    }
}
