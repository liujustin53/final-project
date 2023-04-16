using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    // public, read-only references to cooldown state
    [field: SerializeField] public float cooldown { get; private set; }
    [field: SerializeField] public float countdown { get; private set; }
    public float readiness => Mathf.Clamp01(1 - (countdown / cooldown));
    public bool readyToCast
    {
        get { return countdown <= 0; }
    }

    [SerializeField]
    FireMode fireMode; 

    [SerializeField]
    AudioClip spellSFX;

    private bool firing;

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
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else if (firing && countdown <= 0)
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
        countdown = cooldown;
    }

    protected abstract void Fire();

    public enum FireMode
    {
        ONCE,
        HOLD,
        TOGGLE
    }
}
