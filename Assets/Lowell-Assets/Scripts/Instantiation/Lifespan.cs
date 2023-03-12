using UnityEngine;

public class Lifespan : MonoBehaviour
{
    [SerializeField] private float lifespan = 2.0f;
    [SerializeField, HideInInspector] private Killable killable;

    
    // OnEnable is called when the object is enabled.
    // We use this instead of Start to add compatibility for pooled objects.
    void OnEnable()
    {
        killable.KillThis(lifespan);
    }

    void OnValidate()
    {
        killable = GetComponent<Killable>();
    }
}
