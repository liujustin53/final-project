using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(menuName = "Global Utilities/Object Pool")]
public class GlobalPool: ScriptableObject, Pooler<Killable>
{
    [SerializeField] private Killable prefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10000;
    private ObjectPool<Killable> _pool;

    void Start() {
        _pool = new ObjectPool<Killable>(
            createFunc: () => { return Instantiate(prefab); },
            actionOnGet: shape => { shape.gameObject.SetActive(true); },
            actionOnRelease: shape => { shape.gameObject.SetActive(false); },
            actionOnDestroy: shape => { Destroy(shape.gameObject); },
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public Killable Spawn(Vector3 position, Quaternion rotation = new Quaternion()) {
        Killable obj = _pool.Get();
        obj.gameObject.transform.SetPositionAndRotation(position, rotation);
        return obj;
    }

    // Kills the given Killable, running any pre-death functions (ex: animations, particle effects)
    public void Kill(Killable killable, float delay = 0) {
        killable.Invoke("_Kill", delay);
    }

    // Immediately stows the given Killable.
    public void Release(Killable killable) {
        _pool.Release(killable);
    }
}