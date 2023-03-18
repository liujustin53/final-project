using UnityEngine;
using UnityEngine.Events;

public class VoidEventChannel : ScriptableObject
{
    public UnityAction OnRaised;

    public void Invoke() {
        if (OnRaised != null) OnRaised.Invoke();
    }
}
