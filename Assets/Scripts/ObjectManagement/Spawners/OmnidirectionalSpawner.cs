using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmnidirectionalSpawner : Spawner
{
    [SerializeField]
    Transform point;

    [SerializeField]
    int spawnCount = 4;

    [SerializeField]
    float distanceFromCenter = 1;

    float offset;

    void Awake()
    {
        if (point == null)
        {
            point = transform;
        }
    }

    public override Killable[] Fire()
    {
        offset = 360.0f / spawnCount;
        Killable[] projectiles = new Killable[spawnCount];
        Quaternion rotation = point.rotation;
        for (int i = 0; i < spawnCount; i++)
        {
            projectiles[i] = Spawn(
                point.position + rotation * (Vector3.forward * distanceFromCenter),
                rotation
            )[0];
            rotation *= Quaternion.Euler(new Vector3(0, offset, 0));
        }

        return projectiles;
    }
}
