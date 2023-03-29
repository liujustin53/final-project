using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimOnPlayer : DampedAim
{
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerHitTarget").transform;
    }
}
