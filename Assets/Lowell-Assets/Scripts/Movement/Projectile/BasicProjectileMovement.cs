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
        rigidbody.AddForce(speed * transform.forward, ForceMode.VelocityChange);
    }
}
