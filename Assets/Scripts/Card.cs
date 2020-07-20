using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Att, Def, Support, Talent, Summon}

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject {
    [Header("冷却时间")]
    public int cd;
    [NonSerialized]
    public int cdCurrent;

    public Sprite icon;

    public string cardName;
    [TextArea(3, 5)]
    public string info;

    public CardType type;

    public Effect effect;

    public void Play(Unit caster, Unit target) {
        Debug.Log(string.Format("{0}打出{1}", caster.name, name));

        effect.Trigger(caster, target);
    }
}
