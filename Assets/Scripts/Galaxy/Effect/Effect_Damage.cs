using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Damage")]
public class Effect_Damage : Effect {
    public float amount;

    public override void Trigger() {
        target.TakeDamage(amount);
    }

}
