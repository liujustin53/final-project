using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour
{
    public Sprite sprite;
    [SerializeField, Range(0, 1)] private float _progress;
    public float progress {
        get => _progress;
        set {
            _progress = value;
            recharging.fillAmount = value;
            ready.enabled = value == 1f;
        }
    }
    [SerializeField] Image recharging;
    [SerializeField] Image ready;

    void OnValidate()
    {
        progress = _progress;

        foreach (var imgComponent in GetComponentsInChildren<Image>()) {
            imgComponent.sprite = sprite;
        }
    }
}
