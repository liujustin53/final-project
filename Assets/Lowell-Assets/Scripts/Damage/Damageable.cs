using UnityEngine;
using UnityEngine.Events;

// Allows a GameObject to receive healing or damage.
public interface Damageable {
    Team team { get; }
    [HideInInspector] UnityEvent<int, Element> damageEvent { get; }
    [HideInInspector] UnityEvent<int> healEvent { get; }

    /// Called when something damages you.
    void Damage(int dmg, Element element);

    /// Called when something heals you.
    void Heal(int hp);
}