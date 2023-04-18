using UnityEngine;

/// <summary> Spawns things with the same location and rotation as the specified object. </summary>
public class PointSpawner : Spawner
{
    [SerializeField]
    protected Transform point;

    void Awake()
    {
        if (point == null)
        {
            point = transform;
        }
    }

    public override Killable[] Fire()
    {
        return Spawn(point.position, point.rotation);
    }
}
