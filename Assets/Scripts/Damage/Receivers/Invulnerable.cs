using UnityEngine;
using UnityEngine.Events;

/// Simply produces events when damaged or healed.
[DisallowMultipleComponent]
public class Invulnerable : MonoBehaviour, Damageable
{
    [field: SerializeField]
    public Team team { get; protected set; }

    public virtual void Damage(int dmg, Element damageType)
    {
        Damageable.SendDamageEvent(gameObject, dmg, damageType);
    }

    public virtual void Heal(int hp)
    {
        Damageable.SendHealEvent(gameObject, hp);
    }
}
