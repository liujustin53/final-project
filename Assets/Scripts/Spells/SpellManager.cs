using UnityEngine;

/// Allows you to easily swap out various Spells to the Player's spell slots.
public class SpellManager : MonoBehaviour
{
    [SerializeField] private Spell primaryAttack;

    void PrimaryInvoke() {
        primaryAttack.StartCast();
    }

    void PrimaryCancel() {
        primaryAttack.ReleaseCast();
    }

    void OnEnable()
    {
        InputManager.fire.AddListener(this.PrimaryInvoke);
        InputManager.fireRelease.AddListener(this.PrimaryCancel);
    }

    void OnDisable() {

        InputManager.fire.RemoveListener(this.PrimaryInvoke);
        InputManager.fireRelease.RemoveListener(this.PrimaryCancel);
    }
}