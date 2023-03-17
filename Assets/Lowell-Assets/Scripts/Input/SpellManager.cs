using UnityEngine;

/// Allows you to easily swap out various Spells to the Player's spell slots.
public class SpellManager : MonoBehaviour
{
    [SerializeField] private Spell primaryAttack;

    void Start()
    {
        InputManager.fire.AddListener(this.PrimaryInvoke);
        InputManager.fireRelease.AddListener(this.PrimaryCancel);
    }

    void PrimaryInvoke() {
        Debug.Log("Invoking primary attack");
        primaryAttack.StartCast();
    }

    void PrimaryCancel() {
        primaryAttack.ReleaseCast();
    }
}