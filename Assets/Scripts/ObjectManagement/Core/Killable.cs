using UnityEngine;
using UnityEngine.Events;

// Gives the attached GameObject more advanced death behaviour
public class Killable: MonoBehaviour
{
    [HideInInspector] public Pooler pooler;

    private bool alreadyKilled = false;

    protected virtual void OnEnable()
    {
        alreadyKilled = false;
    }

    /// <summary> Kill or Destroy the given GameObject; </summary>
    /// <remarks> behaves similarly to Destroy </remarks>
    public static void Kill(GameObject toKill, float delay = 0) {
        if (toKill.TryGetComponent<Killable>(out Killable killable)) {
            killable.Kill(delay);
        } else {
            Destroy(toKill, delay);
        }
    }

    /// <summary> Kills this </summary>
    public void Kill(float delay = 0) {
        Invoke("_Kill", delay);
    }

    /// <summary>
    /// Runs when Killed.
    /// </summary>
    /// <returns>
    /// The number of seconds to delay the actual destruction of the object.
    /// </returns>
    public virtual float BeforeKill() { return 0.0f; }

    private void _Kill() {
        if (alreadyKilled) return;
        alreadyKilled = true;
        foreach (KillResponse response in GetComponents<KillResponse>()) {
            response.OnKilled();
        }
        Invoke("_Destroy", BeforeKill());
    }

    private void _Destroy() {
        if (pooler != null) {
            pooler.Release(this);
        } else {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDisable() {
        CancelInvoke();
    }
}
