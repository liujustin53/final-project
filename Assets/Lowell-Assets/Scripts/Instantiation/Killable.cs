using UnityEngine;
using UnityEngine.Events;

public class Killable: MonoBehaviour
{
    [HideInInspector] public Pooler pooler;
    private UnityEvent _killEvent;
    public UnityEvent killEvent => _killEvent;

    private bool alreadyKilled = false;

    protected virtual void Start() {
        _killEvent = new UnityEvent();
    }

    protected virtual void OnEnable()
    {
        alreadyKilled = false;
    }
    
    public static void Kill(GameObject toKill, float delay = 0) {
        if (toKill.TryGetComponent<Killable>(out Killable killable)) {
            Killable.Kill(killable, delay);
        } else {
            Destroy(toKill, delay);
        }
    }

    public static void Kill(Killable toKill, float delay = 0) {
        toKill.Kill(delay);
    }

    public void Kill(float delay = 0) {
        Invoke("_Kill", delay);
    }

    public virtual float BeforeKill() { return 0.0f; }

    private void _Kill() {
        if (alreadyKilled) return;
        alreadyKilled = true;
        killEvent.Invoke();
        Invoke("_Destroy", BeforeKill());
    }

    private void _Destroy() {
        if (pooler != null) {
            pooler.Release(this);
        } else {
            Destroy(gameObject);
        }
    }
}
