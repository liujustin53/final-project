using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPanel : MonoBehaviour
{
    private static SpellPanel _instance;
    public static Transform panel => _instance.transform;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.LogWarning(
                "Multiple spell panels exist: ["
                    + _instance.gameObject.name
                    + "], ["
                    + gameObject.name
                    + "]"
            );
            Destroy(this);
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
