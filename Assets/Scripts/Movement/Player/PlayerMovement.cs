using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected MovementParams movementParams;
    protected ControlParameters controlParameters;
    
    CharacterController controller;
    Animator animator;

    Vector3 horizontalVelocity;
    float verticalVelocity;

    float jumpVel;
    float jumpCountdown;
    float coyoteCountdown;
    bool jumping;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpVel = Mathf.Sqrt(-movementParams.jumpHeight * Physics.gravity.y);
        controlParameters = ControlParameters.instance;
        animator = GetComponent<Animator>();
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

        JumpAndFall();
        HorizontalMovement();
        if (InputManager.move.magnitude > 0.05) {
            FaceForward();
        }

        controller.Move((horizontalVelocity + Vector3.up * verticalVelocity) * Time.deltaTime);
    }

    // Move based on the InputManager's movement axes (local space)
    void HorizontalMovement() {
        Vector3 right = Vector3.Scale(new Vector3(1, 0, 1), Camera.main.transform.right).normalized;
        Vector3 forward = Vector3.Scale(new Vector3(1, 0, 1), Camera.main.transform.forward).normalized;
        Vector3 targetVelocity = InputManager.move.x * right + InputManager.move.y * forward;
        targetVelocity *= movementParams.speed;

        float friction = isGrounded ? movementParams.groundControl : movementParams.airControl;
        horizontalVelocity = DampedControl(horizontalVelocity, targetVelocity, friction);

        animator.SetFloat("Speed", horizontalVelocity.magnitude);
    }

    // Handle vertical velocity management
    void JumpAndFall() {
        if (controller.isGrounded) {
            coyoteCountdown = controlParameters.coyoteTime;
            verticalVelocity = 0;
        }
        isGrounded = coyoteCountdown > 0;
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (coyoteCountdown > 0 && jumpCountdown > 0 && !jumping) {
            verticalVelocity = jumpVel;
            jumping = true;
            animator.SetTrigger("Jump");
        }
        
        if (verticalVelocity <= 0) {
            jumping = false;
        }

        animator.SetBool("Grounded", isGrounded && !jumping);
        
        coyoteCountdown -= Time.unscaledDeltaTime;
        jumpCountdown -= Time.unscaledDeltaTime;
    }

    // Rotate towards the main camera's forward direction (about the y axis)
    void FaceForward() {
        Vector3 eulers = transform.eulerAngles;
        float target = Mathf.Atan2(horizontalVelocity.x, horizontalVelocity.z) * Mathf.Rad2Deg;
        float control = isGrounded ? movementParams.turnControl : 0.0f;
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
