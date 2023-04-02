using UnityEngine;

/// <summary> Limits the GameObject's lifespan. </summary>
public class Lifespan : MonoBehaviour
{
    [SerializeField]
    private float lifespan = 2.0f;

    void OnEnable()
    {
        Killable.Kill(gameObject, lifespan);
    }
}
