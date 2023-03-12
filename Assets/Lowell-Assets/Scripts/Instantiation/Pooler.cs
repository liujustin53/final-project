using UnityEngine;
using UnityEngine.Pool;

public class Pooler: MonoBehaviour
{
    [SerializeField] private Killable _prefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10000;
    private ObjectPool<Killable> _pool;

    public Killable prefab => _prefab;

    void Start() {
        _pool = new ObjectPool<Killable>(
            createFunc: () => { 
                Killable instantiated =  Instantiate(prefab); 
                instantiated.pooler = this;
                return instantiated;
                },
            actionOnGet: shape => { shape.gameObject.SetActive(true); },
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
        obj.gameObject.transform.SetPositionAndRotation(position, rotation);
        return obj;
    }

    // Immediately stows the given Killable.
    public void Release(Killable killable) {
        _pool.Release(killable);
    }
}