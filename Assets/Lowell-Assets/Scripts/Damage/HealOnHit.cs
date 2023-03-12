using UnityEngine;

[DisallowMultipleComponent]
public class HealOnHit : MonoBehaviour
{
    [SerializeField] protected int health = 10;
    [SerializeField] protected Team[] canHeal;

    public void Heal(Damageable other) {
        if (other.team.In(canHeal)) {
            other.Heal(this.health);
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Damageable>(out Damageable damageable)) {
            Heal(damageable);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Damageable>(out Damageable damageable)) {
            Heal(damageable);
        }
    }
}
