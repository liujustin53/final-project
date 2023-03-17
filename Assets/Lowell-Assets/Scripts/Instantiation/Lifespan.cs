using UnityEngine;

/// Limits the GameObject's lifespan.
public class Lifespan : MonoBehaviour
{
    [SerializeField] private float lifespan = 2.0f;
    
    void OnEnable()
    {
        Killable.Kill(gameObject, lifespan);
    }
}
