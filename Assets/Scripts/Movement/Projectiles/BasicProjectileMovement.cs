using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileMovement : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        rigidbody.velocity = speed * transform.forward;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
