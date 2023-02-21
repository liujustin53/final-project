using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    GameObject projectileParent;
    // Start is called before the first frame update
    void Start()
    {
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireAt(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.LookAt(target, Vector3.up);

        GameObject projectile = Instantiate(projectilePrefab, transform.position + direction, transform.rotation);

        projectile.transform.parent = projectileParent.transform;


    }
}
