using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/// <summary>
/// Gives access to the Input System found in Input->PlayerControls through callbacks.
/// </summary>
/// <remarks>
/// This uses Unity's "new" input system, which makes it easier to configure controls globally,
/// rather than hunting through individual scripts.
/// It also provides a base for allowing controller input.
/// </remarks>
[CreateAssetMenu(menuName = "Game/Controls/Input Manager")]
public class InputManager : SingletonScriptableObject<InputManager>, PlayerControls.IPlayerActions
{
    PlayerControls playerControls;
    PlayerControls.PlayerActions playerActions;

    public static Vector2 deltaLook => instance._deltaLook;
    public static Vector2 move => instance._move;
    public static UnityEvent jump => instance._jump;
    public static UnityEvent jumpRelease => instance._jumpRelease;
    public static float deltaZoom => instance._deltaZoom;

    public static UnityEvent pause => instance._pause;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Player;

        playerActions.SetCallbacks(this);
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
        primaryFire.Invoke(context);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jump.Invoke();
        }
        if (context.canceled)
        {
            jumpRelease.Invoke();
        }
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        _deltaZoom = context.ReadValue<float>();
    }

    public void OnEnable()
    {
        if (playerControls != null)
        {
            playerControls.Enable();
        }
    }

    public void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pause.Invoke();
        }
    }

    public void OnBlink(InputAction.CallbackContext context)
    {
        blink.Invoke(context);
    }

    public void OnHeal(InputAction.CallbackContext context)
    {
        heal.Invoke(context);
    }

    void OnValidate()
    {
        Awake();
    }

    public void OnFireball1(InputAction.CallbackContext context)
    {
        secondaryFire.Invoke(context);
    }

    [HideInInspector]
    public Vector2 _deltaLook;

    [HideInInspector]
    public Vector2 _move;

    [HideInInspector]
    public UnityEvent _jump;

    [HideInInspector]
    public UnityEvent _jumpRelease;

    public ButtonEvent primaryFire;
    public ButtonEvent secondaryFire;
    public ButtonEvent blink;
    public ButtonEvent heal;

    [HideInInspector]
    public float _deltaZoom;

    [HideInInspector]
    public UnityEvent _pause;
}
