using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ScriptableObject {
    protected Unit caster, target;

    public virtual void Trigger() {
    }

    public void Trigger(Unit caster, Unit target) {
        this.caster = caster;
        this.target = target;

        Trigger();
    }
}
