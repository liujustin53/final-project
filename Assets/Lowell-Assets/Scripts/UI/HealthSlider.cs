using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected Mortal target;
    // Start is called before the first frame update
    void Start()
    {
        this.healthSlider.minValue = 0;
        this.healthSlider.maxValue = this.target.maxHp;
        this.healthSlider.wholeNumbers = true;
        this.healthSlider.value = this.target.hp;
        this.target.healEvent.AddListener(this.showHeal);
        this.target.damageEvent.AddListener(this.showDamage);
    }

    void showDamage(int dmg, Element element) {
        this.healthSlider.value = this.target.hp;
    }

    // Update is called once per frame
    void showHeal(int hp)
    {
        this.healthSlider.value = this.target.hp;
    }
}
