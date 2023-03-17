using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected MovementParams movementParams;
    protected ControlParameters controlParameters;
    
    CharacterController controller;

    Vector3 horizontalVelocity;
    float verticalVelocity;

    float jumpVel;
    float jumpCountdown;
    float coyoteCountdown;
    bool jumping;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpVel = Mathf.Sqrt(-movementParams.jumpHeight * Physics.gravity.y);
        controlParameters = ControlParameters.instance;
    }

    // Callback for pressing the "Jump" control
    public void OnJump() {
        jumpCountdown = controlParameters.jumpBuffer;
    }
    // Callback for releasing the "Jump" control
    public void OnJumpRelease() {
        coyoteCountdown = 0;
        if (jumping) {
            verticalVelocity *= 0.5f;
        }
        jumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.move.magnitude > 0.05) {
            FaceForward();
        }
        
        HorizontalMovement();
        JumpAndFall();

        controller.Move((horizontalVelocity + Vector3.up * verticalVelocity) * Time.deltaTime);
    }

    // Move based on the InputManager's movement axes (local space)
    void HorizontalMovement() {
        Vector3 targetVelocity = InputManager.move.x * transform.right + InputManager.move.y * transform.forward;
        targetVelocity *= movementParams.speed;

        float friction = controller.isGrounded ? movementParams.groundControl : movementParams.airControl;
        horizontalVelocity = DampedControl(horizontalVelocity, targetVelocity, friction);
    }

    // Handle vertical velocity management
    void JumpAndFall() {
        if (controller.isGrounded) {
            coyoteCountdown = controlParameters.coyoteTime;
            verticalVelocity = 0;
        }

        if (coyoteCountdown > 0 && jumpCountdown > 0) {
            verticalVelocity = jumpVel;
            jumping = true;
        }
        
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        if (verticalVelocity <= 0) {
            jumping = false;
        }

        
        coyoteCountdown -= Time.unscaledDeltaTime;
        jumpCountdown -= Time.unscaledDeltaTime;
    }

    // Rotate towards the main camera's forward direction (about the y axis)
    void FaceForward() {
        Vector3 eulers = transform.eulerAngles;
        float target = Camera.main.transform.eulerAngles.y;
        float control = controller.isGrounded ? movementParams.turnControl : 0.0f;
        float next = Mathf.LerpAngle(eulers.y, target, DampedFac(control));
        transform.rotation = Quaternion.Euler(eulers.x, next, eulers.z);
    }

    // Helpers for damped movement
    float DampedFac(float control) {
        return 1 - Mathf.Pow(1 - control, 4 * Time.deltaTime);
    }
    Vector3 DampedControl(Vector3 current, Vector3 target, float control) {
        return Vector3.Lerp(
            current, 
            target, 
            DampedFac(control)
        );
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
