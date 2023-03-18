using UnityEngine;

public class Spell : MonoBehaviour
{
    // public, read-only references to cooldown state
    public float cooldown => _cooldown;
    public float countdown => _countdown;
    public bool readyToCast { get { return countdown <= 0; } }

    [SerializeField] FireMode fireMode;
    [SerializeField] float _cooldown;
    [SerializeField] Spawner spawner;

    private bool firing;
    private float _countdown;

    void Start()
    {
        if (spawner == null) {
            spawner = GetComponent<Spawner>();
        }
    }

    public void StartCast() {
        if (fireMode == FireMode.TOGGLE) {
            firing = !firing;
        } else {
            firing = true;
        }
        firing = true;
    }

    public void ReleaseCast() {
        if (fireMode != FireMode.TOGGLE) {
            firing = false;
        }
    }

    public void Update()
    {

        if (_countdown > 0) {
            _countdown -= Time.deltaTime;
            
        } else if (firing && _countdown <= 0) {
            Fire();
        }
        if (fireMode == FireMode.ONCE) {
            firing = false;
        }
    }

    private void Fire() {
        spawner.Fire();
        _countdown = _cooldown;
    }

    public enum FireMode {
        ONCE,
        HOLD,
        TOGGLE
    }
}