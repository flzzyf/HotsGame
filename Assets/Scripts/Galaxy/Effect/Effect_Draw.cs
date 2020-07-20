using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Draw")]
public class Effect_Draw : Effect {
    public int amount = 1;

    public override void Trigger() {
        for (int i = 0; i < amount; i++) {
            CardManager.instance.DrawCard(caster.player);
        }
    }

}
