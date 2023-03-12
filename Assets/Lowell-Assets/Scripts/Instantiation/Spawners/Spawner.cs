using System;
using UnityEngine;

public class Spawner: MonoBehaviour
{
    [SerializeField] private bool usePooler = true;
    [SerializeField, DrawIf("usePooler", false)] private Pooler pooler;
    [SerializeField, DrawIf("usePooler", true)] private Killable prefab;

    public virtual Killable Fire() {
        return Spawn(transform.position, transform.rotation);
    }

    protected Killable Spawn(Vector3 pos, Quaternion rot = new Quaternion()) {
        if (usePooler) {
            Killable killable = pooler.Spawn(pos, rot);
            return killable;
        }
        return UnityEngine.Object.Instantiate<Killable>(prefab, pos, rot);
    }
}