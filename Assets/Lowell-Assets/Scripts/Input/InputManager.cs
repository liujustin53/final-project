using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Game/Controls/Input Manager")]
public class InputManager : ScriptableObject, PlayerControls.IPlayerActions
{
    PlayerControls playerControls;
    PlayerControls.PlayerActions playerActions;

    [HideInInspector] public Vector2 deltaLook;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public UnityEvent jump;
    [HideInInspector] public UnityEvent jumpRelease;
    [HideInInspector] public UnityEvent fire;
    [HideInInspector] public UnityEvent dodge;
    [HideInInspector] public float deltaZoom;


    void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Player;

        playerActions.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        deltaLook = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started) {
            fire.Invoke();
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
        deltaZoom = context.ReadValue<float>();
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
}
