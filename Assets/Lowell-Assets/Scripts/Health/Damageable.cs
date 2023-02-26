using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHp = 100;
    [SerializeField] protected Slider healthSlider;

    private int currentHp;

    protected abstract void Die();

    public abstract Team GetTeam();
    
    protected virtual void Start()
    {
        this.currentHp = this.maxHp;

        if (this.healthSlider != null) {
            this.healthSlider.minValue = 0;
            this.healthSlider.maxValue = this.maxHp;
            this.healthSlider.wholeNumbers = true;
            this.healthSlider.value = this.currentHp;
        }
    }

    public virtual void Damage(int hp)
    {
        Debug.Assert(hp >= 0);
        this.currentHp = Mathf.Max(0, currentHp - hp);
        UpdateSlider();
        if (this.currentHp == 0) {
            Die();
        }
    }

    protected virtual void UpdateSlider() {
        if (this.healthSlider != null) {
            healthSlider.value = this.currentHp;
        }
    }

    public virtual void Heal(int hp)
    {
        Debug.Assert(hp >= 0);
        this.currentHp = Mathf.Min(this.maxHp, currentHp + hp);
    }

    protected void OnTriggerEnter(Collider other)
    {
        DamageOnHit damager = other.gameObject.GetComponent<DamageOnHit>();
        if (damager != null) {
            damager.Damage(this);
        }
        HealOnHit healer = other.gameObject.GetComponent<HealOnHit>();
        if (healer != null) {
            healer.Heal(this);
        }
    }
}
