using UnityEngine;

public class BlinkSpell : Spell
{
    [SerializeField]
    private float blinkDistance = 10.0f;

    private PhysicsMover mover;
    private float playerRadius;

    void Start()
    {
        mover = GetComponent<PhysicsMover>();
        playerRadius = GetComponent<CapsuleCollider>().radius;
    }

    protected override void Fire()
    {
        Vector2 direction = mover.Movement.normalized;
        Vector3 direction3D = new Vector3(direction.x, 0, direction.y);
        // raycast to see if we can blink that far
        if (Physics.Raycast(transform.position + Vector3.up, direction3D, out RaycastHit hit, blinkDistance))
        {
            // if we hit something, blink to the point just before the hit
            transform.position = hit.point - direction3D * playerRadius - Vector3.up;
        }
        else
        {
            // if we didn't hit anything, blink the full distance
            transform.position += direction3D * blinkDistance;
        }
    }
}
