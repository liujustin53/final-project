using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpell : MonoBehaviour
{
    [SerializeField] SpellIcon icon;
    [SerializeField] Spell spell;
    [SerializeField] ButtonEvent hotkey;

    void OnEnable()
    {
        if (icon == null) { icon = GetComponent<SpellIcon>(); }
        if (spell == null) { spell = GetComponent<Spell>(); }

        hotkey.press.AddListener(spell.StartCast);
        hotkey.release.AddListener(spell.ReleaseCast);
    }

    void OnDisable()
    {
        hotkey.press.RemoveListener(spell.StartCast);
        hotkey.release.RemoveListener(spell.ReleaseCast);
    }

    // Update is called once per frame
    void Update()
    {
        if (icon == null) { Debug.Log("Null icon: " + gameObject.name); }
        if (spell == null) { Debug.Log("Null spell: " + gameObject.name); }
        icon.progress = spell.readiness;
    }
}
