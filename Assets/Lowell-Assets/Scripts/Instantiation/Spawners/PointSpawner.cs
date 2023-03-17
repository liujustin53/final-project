using UnityEngine;

public class PointSpawner : Spawner
{
    [SerializeField] Transform muzzle;

    void Start() {
        if (muzzle == null) {
            muzzle = transform;
        }
    }

    public override Killable Fire() {
        return Spawn(muzzle.position, muzzle.rotation);
    }
}
