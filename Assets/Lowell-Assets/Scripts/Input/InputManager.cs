using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Game/Controls/Input Manager")]
public class InputManager : ScriptableObject, PlayerControls.IPlayerActions
{
    public static InputManager instance;
    PlayerControls playerControls;
    PlayerControls.PlayerActions playerActions;

    public static Vector2 deltaLook => instance._deltaLook;
    public static Vector2 move => instance._move;
    public static UnityEvent jump => instance._jump;
    public static UnityEvent jumpRelease => instance._jumpRelease;
    public static UnityEvent fire => instance._fire;
    public static UnityEvent fireRelease => instance._fireRelease;
    public static UnityEvent dodge => instance._dodge;
    public static float deltaZoom => instance._deltaZoom;


    void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Player;

        playerActions.SetCallbacks(this);
        instance = this;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _deltaLook = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started) {
            fire.Invoke();
        }
        if (context.canceled) {
            fireRelease.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) {
            jump.Invoke();
        }
        if (context.canceled) {
            jumpRelease.Invoke();
        }
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        _deltaZoom = context.ReadValue<float>();
    }

    public void OnEnable()
    {
        if (playerControls != null) {
            playerControls.Enable();
        }
    }

    public void OnDisable()
    {
        if (playerControls != null) {
            playerControls.Disable();
        }
    }
#if UNITY_EDITOR
    void OnValidate() {
        Awake();
    }
#endif

    [HideInInspector] public Vector2 _deltaLook;
    [HideInInspector] public Vector2 _move;
    [HideInInspector] public UnityEvent _jump;
    [HideInInspector] public UnityEvent _jumpRelease;
    [HideInInspector] public UnityEvent _fire;
    [HideInInspector] public UnityEvent _fireRelease;
    [HideInInspector] public UnityEvent _dodge;
    [HideInInspector] public float _deltaZoom;
}
