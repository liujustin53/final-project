using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavmeshMovement))]
public class Chaser : MonoBehaviour
{
    [SerializeField] Transform target;
    private NavmeshMovement navigator;
    // Start is called before the first frame update
    void Start()
    {
        navigator = GetComponent<NavmeshMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        navigator.SetDestination(target.position);
    }
}
