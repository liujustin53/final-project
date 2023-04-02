using UnityEngine;
using UnityEngine.Pool;

/// <summary> Reduces memory volatility by reusing old GameObjects. </summary>
/// <remarks>
/// For objects being spawned:
/// <br/>Stuff that only needs to be set once should happen in Start();
/// <br/>Stuff that should be reset every time the object is "respawned" should happen in OnEnable()
/// </remarks>
public class Pooler : MonoBehaviour
{
    [SerializeField]
    private Killable _prefab;

    [SerializeField]
    private int defaultCapacity = 10;

    [SerializeField]
    private int maxSize = 10000;

    [SerializeField]
    Transform parent;
    Transform inactiveParent;
    private ObjectPool<Killable> _pool;

    public Killable prefab => _prefab;

    void Start()
    {
        if (parent == null)
        {
            parent = transform;
        }
        var inactiveParentObject = new GameObject(name + "_inactive");
        inactiveParentObject.transform.parent = transform;
        inactiveParentObject.SetActive(false);
        inactiveParent = inactiveParentObject.transform;
        _pool = new ObjectPool<Killable>(
            createFunc: () =>
            {
                Killable instantiated = Instantiate(prefab, inactiveParent);
                instantiated.pooler = this;
                instantiated.gameObject.SetActive(false);
                instantiated.transform.SetParent(parent);
                return instantiated;
            },
            actionOnGet: shape => { },
            actionOnRelease: shape =>
            {
                shape.gameObject.SetActive(false);
            },
            actionOnDestroy: shape =>
            {
                Destroy(shape.gameObject);
            },
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    /// <summary> Spawn an object from the pool. Defaults to the spawner's transform. /summary>
    public Killable Spawn() => Spawn(transform.position, transform.rotation);

    /// <summary> Spawn an object from the pool. Defaults to the spawner's transform. /summary>
    public Killable Spawn(Vector3 position) => Spawn(position, transform.rotation);

    /// <summary> Spawn an object from the pool. Defaults to the spawner's transform. /summary>
    public Killable Spawn(Vector3 position, Quaternion rotation = new Quaternion())
    {
        Killable obj = _pool.Get();
        obj.gameObject.transform.position = position;
        obj.gameObject.transform.rotation = rotation;
        obj.gameObject.SetActive(true);
        return obj;
    }

    /// <summary> Immediately stows the given Killable into the pool.</summary>
    /// <remarks> Should only really be used by Killable </remarks>
    public void Release(Killable killable)
    {
        _pool.Release(killable);
    }
}
