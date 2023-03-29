using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    // public, read-only references to cooldown state
    public float cooldown => _cooldown;
    public float countdown => _countdown;
    public bool readyToCast
    {
        get { return countdown <= 0; }
    }

    [SerializeField]
    FireMode fireMode;

    [SerializeField]
    float _cooldown;

    [SerializeField]
    AudioClip spellSFX;

    private bool firing;
    private float _countdown;

    public void StartCast()
    {
        if (fireMode == FireMode.TOGGLE)
        {
            firing = !firing;
        }
        firing = true;
    }

    public void ReleaseCast()
    {
        if (fireMode != FireMode.TOGGLE)
        {
            firing = false;
        }
    }

    public void Update()
    {
        if (_countdown > 0)
        {
            _countdown -= Time.deltaTime;
        }
        else if (firing && _countdown <= 0)
        {
            _Fire();
        }
        if (fireMode == FireMode.ONCE)
        {
            firing = false;
        }
    }

    private void _Fire()
    {
        Fire();
        if (spellSFX != null)
        {
            AudioSource.PlayClipAtPoint(spellSFX, Camera.main.transform.position);
        }
        _countdown = _cooldown;
    }

    protected abstract void Fire();

    public enum FireMode
    {
        ONCE,
        HOLD,
        TOGGLE
    }
}
