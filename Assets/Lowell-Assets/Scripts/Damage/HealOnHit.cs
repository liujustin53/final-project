using UnityEngine;

[DisallowMultipleComponent]
public class HealOnHit : Killable
{
    [SerializeField] protected int health = 10;
    [SerializeField] protected Team[] canHeal;
    [SerializeField] protected bool fragile = false;

    public void Heal(Damageable other) {
        if (other.team.In(canHeal)) {
            other.Heal(this.health);
            Kill();
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Damageable>(out Damageable damageable)) {
            Heal(damageable);
        }
        if (fragile) Kill();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Damageable>(out Damageable damageable)) {
            Heal(damageable);
        }
    }
}
