using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected MovementParams movementParams;
    
    
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
    }

    public void OnJump() {
        jumpCountdown = movementParams.jumpBuffer;
    }

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

        float control = controller.isGrounded ? movementParams.groundControl : movementParams.airControl;
        if (InputManager.move.magnitude > 0.05) {
            FaceForward();
        }
        Vector3 targetVelocity = InputManager.move.x * transform.right + InputManager.move.y * transform.forward;
        targetVelocity *= movementParams.speed;


        horizontalVelocity = DampedControl(horizontalVelocity, targetVelocity, control);

        JumpAndFall();

        controller.Move((horizontalVelocity + Vector3.up * verticalVelocity) * Time.deltaTime);

        coyoteCountdown -= Time.unscaledDeltaTime;
        jumpCountdown -= Time.unscaledDeltaTime;
    }

    void JumpAndFall() {
        if (controller.isGrounded) {
            coyoteCountdown = movementParams.coyoteTime;
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
    }
    void FaceForward() {
        Vector3 eulers = transform.eulerAngles;
        float target = Camera.main.transform.eulerAngles.y;
        float control = controller.isGrounded ? movementParams.turnControl : 0.0f;
        float next = Mathf.LerpAngle(eulers.y, target, DampedFac(control));
        transform.rotation = Quaternion.Euler(eulers.x, next, eulers.z);
    }

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
