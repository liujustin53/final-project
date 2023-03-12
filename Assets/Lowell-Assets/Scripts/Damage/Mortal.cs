using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Mortal : Killable, Damageable
{
    [SerializeField] protected int _maxHp = 100;
    [SerializeField] protected Element element;
    [SerializeField] protected Team _team;

    UnityEvent<int, Element> _damageEvent;
    UnityEvent<int> _healEvent;
   
    public int maxHp => _maxHp;
    public int hp => _currentHp;
    public Team team => _team;
    public UnityEvent<int, Element> damageEvent => _damageEvent;
    public UnityEvent<int> healEvent => _healEvent;

    private int _currentHp;
    
    protected override void Start()
    {
        base.Start();
        this._currentHp = this.maxHp;
    }

    public virtual void Damage(int dmg, Element damageType)
    {
        Debug.Assert(dmg >= 0);
        int realDamage = dmg;
        if (this.element != null) {
            realDamage = this.element.GetRealDamage(dmg, damageType);
        }
        this.damageEvent.Invoke(realDamage, damageType);

        this._currentHp = Mathf.Max(0, _currentHp - realDamage);
        if (this._currentHp == 0) {
            Killable.Kill(this.gameObject);
        }
    }

    public virtual void Heal(int hp)
    {
        Debug.Assert(hp >= 0);
        hp = Mathf.Min(hp, this.maxHp - this._currentHp);
        this._currentHp = Mathf.Min(this.maxHp, hp);
        this.healEvent.Invoke(hp);
    }

    protected virtual void Die() {
        Killable.Kill(this);
    }
}
