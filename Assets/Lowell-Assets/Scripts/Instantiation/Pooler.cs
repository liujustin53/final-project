using UnityEngine;
using UnityEngine.Pool;

/// Reduces memory volatility by reusing old GameObjects.
/// Best used for things like projectiles or other things that need to be instantiated / destroyed
/// frequently or in large quantities.
public class Pooler: MonoBehaviour
{
    [SerializeField] private Killable _prefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10000;
    [SerializeField] Transform parent;
    private ObjectPool<Killable> _pool;

    public Killable prefab => _prefab;

    void Start() {
        if (parent == null) {
            parent = transform;
        }
        _pool = new ObjectPool<Killable>(
            createFunc: () => { 
                Killable instantiated =  Instantiate(prefab); 
                instantiated.pooler = this;
                instantiated.transform.SetParent(parent);
                return instantiated;
                },
            actionOnGet: shape => { },
            actionOnRelease: shape => { shape.gameObject.SetActive(false); },
            actionOnDestroy: shape => { Destroy(shape.gameObject); },
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public Killable Spawn() => Spawn(transform.position);
    public Killable Spawn(Vector3 position, Quaternion rotation = new Quaternion()) {
        Killable obj = _pool.Get();
        obj.gameObject.transform.position = position;
        obj.gameObject.transform.rotation = rotation;
        obj.gameObject.SetActive(true);
        return obj;
    }

    // Immediately stows the given Killable.
    public void Release(Killable killable) {
        _pool.Release(killable);
    }
}