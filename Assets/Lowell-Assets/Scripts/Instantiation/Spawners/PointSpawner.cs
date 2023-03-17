using UnityEngine;

// Spawns things with the same location and rotation as the specified object.
public class PointSpawner : Spawner
{
    [SerializeField] Transform point;

    void Start() {
        if (point == null) {
            point = transform;
        }
    }

    public override Killable Fire() {
        return Spawn(point.position, point.rotation);
    }
}
