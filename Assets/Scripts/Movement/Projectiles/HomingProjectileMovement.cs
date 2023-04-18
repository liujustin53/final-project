using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectileMovement : BasicProjectileMovement
{
    [SerializeField]
    float turnSpeed = 10f;

    [SerializeField]
    float maxTurnAngle = 45f;

    Transform player;

    float angleTurned;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerHitTarget").transform;
        angleTurned = 0;
    }

    void FixedUpdate()
    {
        if (angleTurned > maxTurnAngle)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        Quaternion newRotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            turnSpeed * Time.deltaTime
        );
        angleTurned += Mathf.Abs(Quaternion.Angle(transform.rotation, newRotation));
        transform.rotation = newRotation;
        rigidbody.velocity = speed * transform.forward;
    }
}
