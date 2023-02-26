using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnHit : MonoBehaviour
{
    [SerializeField] protected int health = 10;
    [SerializeField, EnumFlagsAttribute] protected Team canHeal;

    public void Heal(IDamageable other) {
        if ((int)(canHeal & other.GetTeam()) != 0) {
            other.Heal(this.health);
        }
    }
}
