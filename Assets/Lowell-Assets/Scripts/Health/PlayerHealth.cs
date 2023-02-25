using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, Damageable
{
    public int maxHp = 100;
    
    private int currentHp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    public void Damage(int dmg) {
        Debug.Assert(dmg >= 0);
        this.currentHp = Mathf.Max(currentHp - dmg, 0);
        if (this.currentHp == 0) {
            LevelManager.Lose();
        }
    }

    public void Heal(int hp) {
        Debug.Assert(hp >= 0);
        this.currentHp = Mathf.Min(currentHp + hp, maxHp);
    }

    public Team GetTeam() {
        return Team.Player;
    }
}
