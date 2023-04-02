using UnityEngine;
using System;

// The backbone of the elemental damage system
[CreateAssetMenu(menuName = "Game/Damage/Element")]
public class Element : ScriptableObject
{
    public Element[] vulnerabilities;
    public Element[] resistances;

    [ColorUsage(false, true)]
    public Color damageColor = Color.white;

    public bool In(Element[] elements)
    {
        return Array.Exists<Element>(elements, element => element == this);
    }

    // Calculate the actual damage of an attack, taking resistances and vulnerabilities into account
    public int GetRealDamage(int dmg, Element type)
    {
        if (type.In(this.vulnerabilities))
        {
            return dmg * 2;
        }
        if (type.In(this.resistances))
        {
            return dmg / 2;
        }
        return dmg;
    }
}
