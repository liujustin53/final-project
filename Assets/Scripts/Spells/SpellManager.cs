using UnityEngine;

/// Allows you to easily swap out various Spells to the Player's spell slots.
public class SpellManager : MonoBehaviour
{
    [SerializeField]
    private Spell primaryAttack;

    [SerializeField]
    private Spell blinkSpell;

    [SerializeField]
    private Spell healSpell;

    [SerializeField]
    private Spell fireBallSpell;

    void PrimaryInvoke()
    {
        primaryAttack.StartCast();
    }

    void PrimaryCancel()
    {
        primaryAttack.ReleaseCast();
    }

    void BlinkInvoke()
    {
        blinkSpell.StartCast();
    }

    void HealInvoke()
    {
        healSpell.StartCast();
    }

    void FireballInvoke()
    {
        fireBallSpell.StartCast();
    }

    void OnEnable()
    {
        /*
        InputManager.fire.AddListener(this.PrimaryInvoke);
        InputManager.fireRelease.AddListener(this.PrimaryCancel);
        InputManager.blink.AddListener(this.BlinkInvoke);
        InputManager.heal.AddListener(this.HealInvoke);
        InputManager.fireball.AddListener(this.FireballInvoke);
        */
    }

    void OnDisable()
    {
        /*
        InputManager.fire.RemoveListener(this.PrimaryInvoke);
        InputManager.fireRelease.RemoveListener(this.PrimaryCancel);
        InputManager.blink.RemoveListener(this.BlinkInvoke);
        InputManager.heal.RemoveListener(this.HealInvoke);
        InputManager.fireball.RemoveListener(this.FireballInvoke);
        */
    }
}
