using UnityEngine;

public class AttackSpell : Spell
{
    [SerializeField]
    Spawner spawner;

    void Start()
    {
        if (spawner == null)
        {
            spawner = GetComponent<Spawner>();
        }
    }

    protected override void Fire()
    {
        spawner.Fire();
    }
}
