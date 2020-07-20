using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Def")]
public class Effect_Def : Effect {
    public int amount;

    public override void Trigger() {
        caster.Def(amount);
    }

}
