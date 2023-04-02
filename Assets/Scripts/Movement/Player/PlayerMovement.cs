using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PhysicsMover mover;

    float jumpCountdown;
    float coyoteCountdown;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<PhysicsMover>();
    }

    // Callback for pressing the "Jump" control
    public void OnJump()
    {
        jumpCountdown = ControlParameters.jumpBuffer;
    }

    // Callback for releasing the "Jump" control
    public void OnJumpRelease()
    {
        coyoteCountdown = 0;
        mover.CancelJump();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FaceForward();
        TryJump();
    }

    void Move()
    {
        Transform view = Camera.main.transform;

        Vector2 right = new Vector2(view.right.x, view.right.z).normalized;
        Vector2 forward = new Vector2(view.forward.x, view.forward.z).normalized;

        mover.Movement = InputManager.move.x * right + InputManager.move.y * forward;
    }

    // Rotate towards the main camera's forward direction (about the y axis)
    void FaceForward()
    {
        if (mover.velocityXZ.magnitude < 0.1)
            return;
        if (!mover.isGrounded)
            return;

        float target = Mathf.Atan2(mover.velocityXZ.x, mover.velocityXZ.y) * Mathf.Rad2Deg;
        mover.TargetRotation = Quaternion.Euler(0, target, 0);
    }

    void TryJump()
    {
        if (mover.isGrounded)
        {
            coyoteCountdown = ControlParameters.coyoteTime;
        }
        if (jumpCountdown > 0 && coyoteCountdown > 0)
        {
            mover.Jump();
        }
        coyoteCountdown -= Time.unscaledDeltaTime;
        jumpCountdown -= Time.unscaledDeltaTime;
    }

    // Helpers for damped movement
    float DampedFac(float control)
    {
        return 1 - Mathf.Pow(1 - control, 4 * Time.deltaTime);
    }

    Vector3 DampedControl(Vector3 current, Vector3 target, float control)
    {
        return Vector3.Lerp(current, target, DampedFac(control));
    }

    // Subscribe / unsubscribe to jump callbacks
    void OnEnable()
    {
        InputManager.jump.AddListener(OnJump);
        InputManager.jumpRelease.AddListener(OnJumpRelease);
    }

    void OnDisable()
    {
        InputManager.jump.RemoveListener(OnJump);
        InputManager.jumpRelease.RemoveListener(OnJumpRelease);
    }
}
