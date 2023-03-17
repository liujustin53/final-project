using UnityEngine;

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