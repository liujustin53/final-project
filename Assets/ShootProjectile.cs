using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireAt(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position + direction, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.AddForce(direction * projectileSpeed, ForceMode.VelocityChange);
    }
}
