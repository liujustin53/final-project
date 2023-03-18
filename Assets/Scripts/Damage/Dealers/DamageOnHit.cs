using UnityEngine;

/// Damages a Damageable when it collides with one.
[DisallowMultipleComponent]
public class DamageOnHit : Killable
{
    [SerializeField] protected int damage = 10;
    [SerializeField] protected Element damageType;
    [SerializeField] protected Team[] canDamage;
    [SerializeField] bool dieOnHit;
    [SerializeField] bool dieOnMiss;

    /// <summary> Damage the given Damageable, and self-destruct if dieOnHit </summary>
    /// <returns> true if it succeeded in damaging the target </returns>
    protected bool Damage(Damageable other) {
        if (other.team == null) {
            Debug.LogError("Null team");
        }
        if (other.team.In(canDamage)) {
            other.Damage(this.damage, this.damageType);
            if (dieOnHit) Kill();
            return true;
        }
        return false;
    }

    void OnCollisionEnter(Collision other)
    {
        bool hit = false;
        if (other.gameObject.TryGetComponent<Damageable>(out Damageable damageable)) {
            hit = Damage(damageable);
        }
        if (!hit && dieOnMiss) {
            Kill();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Damageable>(out Damageable damageable)) {
            Damage(damageable);
        }
    }
}
