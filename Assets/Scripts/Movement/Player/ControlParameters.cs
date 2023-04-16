using UnityEngine;

/// Globally adjusts player control settings.
[CreateAssetMenu(menuName = "Game/Controls/Control Parameters")]
public class ControlParameters : SingletonScriptableObject<ControlParameters>
{
    public static float lookSensitivity { 
                                        get { return instance._lookSensitivity; } 
                                        set { instance._lookSensitivity = value; } }
    public static bool invertX => instance._invertX;
    public static bool invertY => instance._invertY;

    public static float zoomSensitivity => instance._zoomSensitivity;
    public static float zoomSmoothing => instance._zoomSmoothing;
    public static float minCameraDistance => instance._minCameraDistance;
    public static float maxCameraDistance => instance._maxCameraDistance;

    /// <summary> The Player can start jumping for this long after leaving the ground </summary>
    public static float coyoteTime => instance._coyoteTime;

    /// <summary> The Player will try to jump for this long after first pressing the button </summary>
    public static float jumpBuffer => instance._jumpBuffer;

    [SerializeField]
    private float _lookSensitivity = 1;

    [SerializeField]
    private bool _invertX;

    [SerializeField]
    private bool _invertY;

    [SerializeField]
    private float _zoomSensitivity = 0.25f;

    [SerializeField]
    private float _zoomSmoothing = 0.5f;

    [SerializeField]
    private float _minCameraDistance = 1.5f;

    [SerializeField]
    private float _maxCameraDistance = 10f;

    [SerializeField]
    private float _coyoteTime = 0.2f;

    [SerializeField]
    private float _jumpBuffer = 0.2f;
}
