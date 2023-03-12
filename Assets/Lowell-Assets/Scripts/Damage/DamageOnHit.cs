using UnityEngine;

[DisallowMultipleComponent]
public class DamageOnHit : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    [SerializeField] protected Element damageType;
    [SerializeField] protected Team[] canDamage;

    public void Damage(Damageable other) {
        if (other.team.In(canDamage)) {
            other.Damage(this.damage, this.damageType);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Damageable>(out Damageable damageable)) {
            Damage(damageable);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Damageable>(out Damageable damageable)) {
            Damage(damageable);
        }
    }
}
