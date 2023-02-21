using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootProjectile : MonoBehaviour
{
    ShootProjectile shooter;
    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<ShootProjectile>();

        InvokeRepeating("OmniDirectionalShot", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OmniDirectionalShot()
    {
        shooter.FireAt(transform.position + transform.forward * 3);
        transform.Rotate(Vector3.up, 45);

        shooter.FireAt(transform.position + transform.forward * 3);
        transform.Rotate(Vector3.up, 45);

        shooter.FireAt(transform.position + transform.forward * 3);
        transform.Rotate(Vector3.up, 45);

        shooter.FireAt(transform.position + transform.forward * 3);
        transform.Rotate(Vector3.up, 45);

        shooter.FireAt(transform.position + transform.forward * 3);
        transform.Rotate(Vector3.up, 45);

        shooter.FireAt(transform.position + transform.forward * 3);
        transform.Rotate(Vector3.up, 45);

        shooter.FireAt(transform.position + transform.forward * 3);
        transform.Rotate(Vector3.up, 45);

        shooter.FireAt(transform.position + transform.forward * 3);
        transform.Rotate(Vector3.up, 45);
    }
}
