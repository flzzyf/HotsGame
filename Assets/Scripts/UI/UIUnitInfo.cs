using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitInfo : MonoBehaviour {
    public Image image_Portrait;
    public Text text_Danger;

    public Text text_Name;
    public Text text_DamageAmount;
    public Slider slider_HP;
    public Text text_HP;

    Unit unit;

    public void Set(Unit unit) {
        this.unit = unit;

        text_Name.text = unit.unitType.unitName;
        text_DamageAmount.text = unit.unitType.damage.ToString();
        image_Portrait.sprite = unit.unitType.portrait;
        slider_HP.value = unit.hp / unit.hpMax;
        text_HP.text = string.Format("{0}/{1}", unit.hp, unit.hpMax);

        //注册血条变化事件
        unit.onDamage += OnHPChanged;
    }

    public void Clear() {
        unit.onDamage -= OnHPChanged;

        unit = null;
    }

    void OnHPChanged(float amount, float hp, float hpMax) {
        slider_HP.value = hp / hpMax;
        text_HP.text = string.Format("{0}/{1}", hp, hpMax);
    }
}
