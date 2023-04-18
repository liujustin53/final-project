using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConeSpawner : PointSpawner
{
    [SerializeField]
    int spawnCount = 3;
    
    [SerializeField]
    float bulletSpread = 30;

    public override Killable[] Fire()
    {
        Killable[] bullets = new Killable[spawnCount];
        Quaternion rotation = point.rotation * Quaternion.Euler(0, -bulletSpread / 2, 0);
        for (int i = 0; i < spawnCount; i++)
        {
            bullets[i] = Spawn(
                point.position + rotation * (Vector3.forward * 2),
                rotation
            )[0];
            rotation *= Quaternion.Euler(0, bulletSpread / (spawnCount - 1), 0);
        }
        return bullets;
    }
}