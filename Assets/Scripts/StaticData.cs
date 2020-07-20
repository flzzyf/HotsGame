using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StaticData")]
public class StaticData : ScriptableObject {
    public CardTypeIcon cardTypeIcon;

    public static StaticData instance {
        get {
            return Resources.Load("StaticData") as StaticData;
        }
    }

    public Sprite GetCardTypeIcon(CardType type) {
        switch (type) {
            case CardType.Att:
                return cardTypeIcon.att;
            case CardType.Def:
                return cardTypeIcon.def;
            case CardType.Support:
                return cardTypeIcon.support;
            case CardType.Talent:
                return cardTypeIcon.def;
            case CardType.Summon:
                return cardTypeIcon.unit;
        }

        return null;
    }
}

[Serializable]
public struct CardTypeIcon {
    public Sprite att, def, support, talent, unit;
} 
