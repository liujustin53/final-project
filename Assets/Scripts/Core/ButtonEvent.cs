using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

[CreateAssetMenu(menuName = "Game/Controls/ButtonEvent")]
public class ButtonEvent : ScriptableObject
{
    [NonSerialized]
    public UnityEvent press = new UnityEvent();

    [NonSerialized]
    public UnityEvent release = new UnityEvent();

    public void Invoke(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            press.Invoke();
        }
        else if (context.canceled)
        {
            release.Invoke();
        }
    }
}
