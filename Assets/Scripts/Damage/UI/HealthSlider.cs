using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour, DamageListener, HealListener
{
    [SerializeField]
    protected Slider healthSlider;
    private Mortal mortal;

    // Start is called before the first frame update
    void Start()
    {
        this.mortal = GetComponent<Mortal>();

        this.healthSlider.minValue = 0;
        this.healthSlider.maxValue = this.mortal.maxHp;
        this.healthSlider.wholeNumbers = true;
        this.healthSlider.value = this.mortal.hp;
    }

    public void OnDamage(int dmg, Element element)
    {
        this.healthSlider.value = this.mortal.hp;
    }

    // Update is called once per frame
    public void OnHeal(int hp)
    {
        this.healthSlider.value = this.mortal.hp;
    }
}
