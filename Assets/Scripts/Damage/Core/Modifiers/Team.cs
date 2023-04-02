using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/Damage/Team")]
public class Team : ScriptableObject
{
    public bool In(Team[] teams)
    {
        return Array.Exists<Team>(teams, team => team == this);
    }
}
