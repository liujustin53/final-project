using UnityEngine;
using UnityEngine.Events;

// Allows a GameObject to receive healing or damage.
public interface Damageable {
    Team team { get; }

    /// Called when something damages you.
    void Damage(int dmg, Element element);

    /// Called when something heals you.
    void Heal(int hp);

    protected static void SendDamageEvent(GameObject obj, int dmg, Element element) {
        foreach (DamageListener listener in obj.GetComponents<DamageListener>()) {
            listener.OnDamage(dmg, element);
        }
    }

    protected static void SendHealEvent(GameObject obj, int hp) {
        foreach (HealListener listener in obj.GetComponents<HealListener>()) {
            listener.OnHeal(hp);
        }
    }
}