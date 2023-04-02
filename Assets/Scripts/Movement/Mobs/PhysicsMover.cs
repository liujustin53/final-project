using UnityEngine;

public class PhysicsMover : MonoBehaviour
{
    [SerializeField]
    MovementParams movementParams;

    [SerializeField]
    float rideHeight = 0.25f;

    [SerializeField]
    float stickyLength = 0.15f;

    // Current state
    public Quaternion TargetRotation = Quaternion.identity;
    public Vector2 Movement;
    public bool isGrounded { get; protected set; }
    public Vector3 velocity => rigidbody.velocity;
    public Vector2 velocityXZ
    {
        get => new Vector2(velocity.x, velocity.z);
    }

    // Ground collision
    float collisionRadius;
    float rayLength;
    float rayStart;
    const float skin = 0.05f;

    bool isJumping;
    float freezeCountdown;

    new Rigidbody rigidbody;
    JumpListener[] jumpListeners;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Collider collider = GetComponent<Collider>();
        collisionRadius = Mathf.Min(collider.bounds.extents.x, collider.bounds.extents.z) - skin;
        rayStart = collider.bounds.extents.y - collisionRadius - skin - collider.bounds.center.y;
        rayLength = rideHeight + stickyLength + skin;
        rigidbody.maxAngularVelocity = 16;
        jumpListeners = GetComponents<JumpListener>();
    }

    public void Jump()
    {
        if (isJumping)
            return;
        rigidbody.velocity = new Vector3(
            rigidbody.velocity.x,
            Mathf.Sqrt(-2 * Physics.gravity.y * movementParams.jumpHeight),
            rigidbody.velocity.z
        );
        foreach (var listener in jumpListeners)
        {
            listener.OnJump();
        }
        isJumping = true;
    }

    public void CancelJump()
    {
        if (isJumping && rigidbody.velocity.y > 0)
        {
            rigidbody.AddForce(Vector3.down * (0.5f * rigidbody.velocity.y));
            isJumping = false;
        }
    }

    public void Freeze(float duration)
    {
        freezeCountdown = duration;
    }

    void FixedUpdate()
    {
        ApplySuspension();

        CorrectRotation();

        ActuateMovement();
    }

    void ActuateMovement()
    {
        Vector3 desiredVelocity;
        if (freezeCountdown <= 0)
        { // Not frozen
            desiredVelocity = new Vector3(
                movementParams.maxSpeed * Movement.x,
                rigidbody.velocity.y,
                movementParams.maxSpeed * Movement.y
            );
        }
        else if (isGrounded)
        { // Stop if frozen on the ground
            desiredVelocity = new Vector3(0, rigidbody.velocity.y, 0);
        }
        else
        { // No movement change in the air
            desiredVelocity = rigidbody.velocity;
        }
        freezeCountdown -= Time.fixedDeltaTime;

        float vDot = Vector3.Dot(rigidbody.velocity.normalized, desiredVelocity.normalized);

        float acceleration = movementParams.acceleration; // * (Mathf.SmoothStep(0, -1, vDot) + 1);

        Vector3 goalVelocity = Vector3.MoveTowards(
            rigidbody.velocity,
            desiredVelocity,
            acceleration * Time.fixedDeltaTime
        );

        Vector3 force = (goalVelocity - rigidbody.velocity) / Time.fixedDeltaTime;
        float maxForce = isGrounded
            ? movementParams.maxAccelForce
            : movementParams.airMaxAccelForce;
        force *= Mathf.Min(1, maxForce / force.magnitude);

        rigidbody.AddForce(force, ForceMode.Acceleration);
    }

    void ApplySuspension()
    {
        if (rigidbody.velocity.y <= 0)
        {
            isJumping = false;
        }
        Vector3 castStart = transform.position + (Vector3.down * rayStart);
        RaycastHit hit;
        if (!Physics.SphereCast(castStart, collisionRadius, Vector3.down, out hit, rayLength))
        {
            isGrounded = false;
            return;
        }

        isGrounded = true;
        isJumping = false;
        float height = hit.distance - skin;
        float disp = rideHeight - height;

        float velocity = -rigidbody.velocity.y;
        float otherVelocity = hit.rigidbody == null ? 0 : -hit.rigidbody.velocity.y;

        float relVel = otherVelocity - velocity;

        float springForce =
            (
                (disp * movementParams.suspensionStrength)
                - (relVel * movementParams.suspensionDampening)
            ) * rigidbody.mass;
        springForce -= rigidbody.mass * Physics.gravity.y;

        rigidbody.AddForce(Vector3.up * springForce);
        if (hit.rigidbody != null && springForce > 0)
        {
            hit.rigidbody.AddForceAtPosition(Vector3.down * springForce, hit.point);
        }
    }

    void CorrectRotation()
    {
        //rigidbody.AddTorque(-rigidbody.angularVelocity * movementParams.angularDampening, ForceMode.Acceleration);

        Quaternion toTarget = GetAngularOffset(transform.rotation, TargetRotation);

        //convert to angle axis representation so we can do math with angular velocity
        Vector3 axis;
        float angle;
        toTarget.ToAngleAxis(out angle, out axis);

        axis.Normalize();
        angle *= Mathf.Deg2Rad;

        Vector3 acceleration = axis * (angle / Time.fixedDeltaTime);
        acceleration -= rigidbody.angularVelocity;

        acceleration *= Mathf.Min(1, movementParams.angularAcceleration / acceleration.magnitude);
        //to multiply with inertia tensor local then rotationTensor coords
        rigidbody.angularVelocity += acceleration;
        rigidbody.angularVelocity -=
            rigidbody.angularVelocity * (movementParams.angularDampening * Time.fixedDeltaTime);
    }

    Quaternion GetAngularOffset(Quaternion current, Quaternion target)
    {
        if (Quaternion.Dot(current, target) > 0)
        {
            return target * Quaternion.Inverse(current);
        }
        return target * Quaternion.Inverse(OtherQuat(current));
    }

    Quaternion OtherQuat(Quaternion quat)
    {
        return new Quaternion(-quat.x, -quat.y, -quat.z, -quat.w);
    }

    public interface JumpListener
    {
        void OnJump();
    }
}
