using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    [NonSerialized]
    public int player;

    private void Awake() {
        if(unitType != null)
            InitUnitType(unitType); ;
    }

    #region 生命

    [NonSerialized]
    public float hpMax;
    [NonSerialized]
    public float hp;

    public event Action<float, float, float> onDamage;

    void InitHp(float hpMax) {
        hp = hpMax;
        this.hpMax = hpMax;
    }

    public void TakeDamage(float amount) {
        //计算防御
        CalculateDamage(ref amount);

        if (amount <= 0)
            return;

        hp -= amount;

        onDamage?.Invoke(amount, hp, hpMax);

        if(hp <= 0) {
            Die();
        }
    }

    #endregion

    #region 防御

    Queue<int> defenceQueue = new Queue<int>();

    //防御
    public void Def(int amount) {
        defenceQueue.Enqueue(amount);
    }

    public void CalculateDamage(ref float amount) {
        if(defenceQueue.Count > 0) {
            int def = defenceQueue.Dequeue();
            amount -= def;
        }
    }

    #endregion

    #region 死亡

    [NonSerialized]
    public bool isDead;

    public void Die() {
        if (isDead) {
            return;
        }

        isDead = true;

        Destroy(gameObject);
    }

    #endregion

    #region 移动

    public float speed = 7;

    public void Move(Vector2 dir) {
        transform.Translate(dir * speed * Time.fixedDeltaTime);
    }

    #endregion

    #region 单位类型

    public UnitType unitType;

    public void InitUnitType(UnitType unitType) {
        InitHp(unitType.hpMax);
    }

    #endregion
}
