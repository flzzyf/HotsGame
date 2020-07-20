using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    CardManager cardManager;

    public Unit enemy;
    List<Card> enemyCards = new List<Card>();

    private void Awake() {
        cardManager = GetComponent<CardManager>();
    }

    //战斗开始
    public void BattleStart(Unit player, Unit enemy) {
        player.player = 0;
        enemy.player = 1;

        this.enemy = enemy;

        cardManager.Init(player, enemy);

        TurnStart();
    }

    //回合开始
    public void TurnStart() {
        //敌人抽牌

        //敌方抽牌
        cardManager.DrawCard(1, 3);

        foreach (var item in cardManager.playerCards[1].cards) {
            enemyCards.Add(item);
        }

        //玩家抽牌
        cardManager.DrawCard(0, 3);

        //开始玩家操作

    }

    //回合结束
    public void TurnEnd() {
        //结束玩家操作

        //结算
        ExecuteCards(() => {
            //手牌洗回牌库
            cardManager.ShuffleCards(0);
            cardManager.ShuffleCards(1);

            //开始回合
            TurnStart();
        });
    }

    void ExecuteCards(Action onComplete = null) {
        //获取玩家待打出牌
        List<UICard> uiCards = cardManager.selectedUICards;
        List<Card> cards = new List<Card>();
        foreach (var item in uiCards) {
            cards.Add(item.card);
        }

        //开始结算双方牌
        while(cards.Count > 0 || enemyCards.Count > 0) {
            Unit player = GameManager.instance.player;
            Unit enemy = GameManager.instance.enemy;

            if (cards.Count > 0) {
                Card card = cards[0];
                cards.RemoveAt(0);

                card.Play(player, enemy);
            }
            if(enemyCards.Count > 0) {
                Card card = enemyCards[0];
                enemyCards.RemoveAt(0);

                card.Play(enemy, player);
            }
        }

        onComplete?.Invoke();
    }
}
