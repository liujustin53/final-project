using UnityEngine;
using UnityEngine.Events;

public class Killable: MonoBehaviour
{
    [SerializeField] private Pooler<Killable> pooler;
    private UnityEvent _killEvent;
    public UnityEvent killEvent => _killEvent;

    protected virtual void Start() {
        _killEvent = new UnityEvent();
    }
    
    public static void Kill(GameObject toKill, float delay = 0) {
        if (toKill.TryGetComponent<Killable>(out Killable killable)) {
            Killable.Kill(killable, delay);
        } else {
            Destroy(toKill, delay);
        }
    }

    public static void Kill(Killable toKill, float delay = 0) {
        toKill.Invoke("_Kill", delay);
    }

    public void KillThis(float delay = 0) {
        Invoke("_Kill", delay);
    }

    public virtual float BeforeKill() { return 0.0f; }

    private void _Kill() {
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
