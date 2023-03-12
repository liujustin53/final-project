using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    const string emissionKey = "_EmissionColor";
    public float frequency = 10;
    public float duration = 0.25f;

    new Renderer renderer;
    MaterialPropertyBlock propBlock;
    float countdown = 0;
    float phase = 0;
    Color flickerColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    Color initialEmission;

    public Team team => throw new System.NotImplementedException();

    // Start is called before the first frame update
    void Start()
    {
        propBlock = new MaterialPropertyBlock();
        renderer = GetComponent<Renderer>();

        renderer.sharedMaterial.EnableKeyword("_EMISSION");
        initialEmission = renderer.sharedMaterial.GetColor("_EmissionColor");

        Damageable damageable = GetComponent<Damageable>();
        if (damageable == null) {
            Debug.Log("Null Damageable?");
        }
        damageable.damageEvent.AddListener(Damage);
        damageable.healEvent.AddListener(Heal);
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0) {
            phase = 0;
        } else {
            phase = (phase + Time.unscaledDeltaTime * frequency) % 1.0f;
        }
        
        renderer.GetPropertyBlock(propBlock);
        float fac = (phase < 0.5f && countdown > 0) ? 1.0f : 0.0f;
        propBlock.SetColor("_EmissionColor", Color.Lerp(initialEmission, flickerColor, fac));
        renderer.SetPropertyBlock(propBlock);

        countdown -= Time.deltaTime;
    }

    public void Damage(int dmg, Element element)
    {
        countdown = duration;
        flickerColor = element.damageColor;
    }

    public void Heal(int hp) {}
}
