using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    Vector2 input;

    public Unit player;
    public Unit enemy;

    CardManager cardManager;
    BattleManager battleManager;

    public UIDialog uiDialog;
    public Dialog dialog;

    public UIUnitInfo unitInfo;
    public UIUnitInfo unitInfo_Enemy;

    private void Awake() {
        cardManager = GetComponent<CardManager>();

        battleManager = GetComponent<BattleManager>();
    }

    private void Start() {
        unitInfo.Set(player);
        unitInfo_Enemy.Set(enemy);

        battleManager.BattleStart(player, enemy);
    }

    void Update() {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown("f")) {
            //uiDialog.ShowText(dialog);

            enemy.Def(3);
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null) {
                print(hit.collider.gameObject.name);
            }
        }
    }

    private void FixedUpdate() {
        if (player.isDead)
            return;

        player.Move(input.normalized);
    }
}
