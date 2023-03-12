using UnityEngine;
using UnityEngine.Events;

public interface Damageable {
    Team team { get; }
    [HideInInspector] UnityEvent<int, Element> damageEvent { get; }
    [HideInInspector] UnityEvent<int> healEvent { get; }
    void Damage(int dmg, Element element);
    void Heal(int hp);
}