using UnityEngine;

public class Lifespan : MonoBehaviour
{
    [SerializeField] private float lifespan = 2.0f;
    
    // OnEnable is called when the object is enabled.
    // We use this instead of Start to add compatibility for pooled objects.
    void OnEnable()
    {
        Killable.Kill(gameObject, lifespan);
    }
}
