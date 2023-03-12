using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Invulnerable : MonoBehaviour, Damageable
{
    [SerializeField] protected Team _team;
    protected UnityEvent<int, Element> _damageEvent;
    protected UnityEvent<int> _healEvent;

    public Team team => _team;
    public UnityEvent<int, Element> damageEvent => _damageEvent;
    public UnityEvent<int> healEvent => _healEvent;

    void Start()
    {
        if (_damageEvent == null) {
            _damageEvent = new UnityEvent<int, Element>();
        }
        if (_healEvent == null) {
            _healEvent = new UnityEvent<int>();
        }
    }

    public virtual void Damage(int dmg, Element damageType)
    {
        this.damageEvent.Invoke(dmg, damageType);
    }

    public virtual void Heal(int hp)
    {
        this.healEvent.Invoke(hp);
    }
}
