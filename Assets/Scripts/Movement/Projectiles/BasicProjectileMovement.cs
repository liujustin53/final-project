using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileMovement : MonoBehaviour
{
    [SerializeField]
    protected float speed = 10f;

    [SerializeField]
    protected Vector2 inaccuracyModifier = Vector2.zero;

    new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform.rotation *= Quaternion.Euler(
            Random.Range(-1f, 1f) * inaccuracyModifier.x,
            Random.Range(-1f, 1f) * inaccuracyModifier.y,
            0
        );
    }

    void OnEnable()
    {
        rigidbody.velocity = speed * transform.forward;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
