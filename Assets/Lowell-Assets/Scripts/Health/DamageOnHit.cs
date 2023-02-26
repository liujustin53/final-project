using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    [SerializeField, EnumFlagsAttribute] protected Team canDamage;

    public void Damage(IDamageable other) {
        Debug.Log(canDamage & other.GetTeam());
        if ((int)(canDamage & other.GetTeam()) != 0) {
            Debug.Log("Dealt damage");
            other.Damage(this.damage);
        } else {
            Debug.Log("Didn't deal damage");
        }
    }
}
