using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : Spell
{
    [SerializeField]
    private int healAmount = 10;

    private Mortal player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Mortal>();
    }

    protected override void Fire()
    {
        player.Heal(healAmount);
    }
}
