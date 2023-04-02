using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    public Vector3 angularVelocity;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(angularVelocity * Time.deltaTime);
    }
}
