using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDampedAim : DampedAim
{
    // Start is called before the first frame update
    void Start()
    {
        alignYAxisWith = Camera.main.transform;
    }
}
