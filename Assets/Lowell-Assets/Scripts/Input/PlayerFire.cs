using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private Spawner spawner;

    void Start()
    {
        input.fire.AddListener(Fire);
    }

    void Fire() {
        spawner.Fire();
    }
}