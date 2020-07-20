using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitType")]
public class UnitType :ScriptableObject {
    public float hpMax = 1;
    public string unitName = "Anon";
    public int damage = 1;
    public Sprite portrait;

    //预设牌组
    public Card[] cards;
}
