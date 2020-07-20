using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : Singleton<CardManager> {
    public UICard cardPrefab;

    //交战双方玩家卡牌
    [NonSerialized]
    public PlayerCards[] playerCards;

    //UI卡牌父级
    public Transform[] cardParents;

    public void Init(Unit player, Unit enemy) {
        playerCards = new PlayerCards[2];
        playerCards[0] = new PlayerCards(player.unitType.cards);
        playerCards[1] = new PlayerCards(enemy.unitType.cards);

        Shuffle(ref playerCards[0].deck);
        Shuffle(ref playerCards[1].deck);
    }

    #region UI手牌

    //创建UI手牌
    UICard AddUICard(int player, Card card) {
        UICard uiCard = Instantiate(cardPrefab, cardParents[player]).GetComponent<UICard>();
        uiCard.Set(card);

        //注册点击事件
        uiCard.onClick += () => {
            OnUICardClick(uiCard);
        };

        return uiCard;
    }

    void RemoveUICard(UICard uiCard) {
        Destroy(uiCard.gameObject);
    }

    //清除UI手牌
    void ClearUICard(int player) {
        Transform cardParent = cardParents[player];
        int cardCount = cardParent.childCount;
        for (int i = 0; i < cardCount; i++) {
            DestroyImmediate(cardParent.GetChild(0).gameObject);
        }
    }

    //当点击卡牌
    public void OnUICardClick(UICard uiCard) {
        //PlayCard(uiCard);

        if (uiCard.isSelected) {
            DeselectCard(uiCard);
        } else {
            SelectCard(uiCard);
        }
    }

    #region 选择UI卡牌

    [NonSerialized]
    public List<UICard> selectedUICards = new List<UICard>();

    public Button button_EndTurn;

    //选择和取消选择UI卡牌
    void SelectCard(UICard uiCard) {
        uiCard.Select(selectedUICards.Count + 1);

        selectedUICards.Add(uiCard);

        button_EndTurn.GetComponentInChildren<Text>().text = "打出手牌";
    }
    void DeselectCard(UICard uiCard) {
        uiCard.Deselect();

        selectedUICards.Remove(uiCard);

        ReorderSelectedCards();

        if(selectedUICards.Count == 0) {
            button_EndTurn.GetComponentInChildren<Text>().text = "结束回合";
        }
    }

    //重新设置选择的卡牌序号
    void ReorderSelectedCards() {
        for (int i = 0; i < selectedUICards.Count; i++) {
            selectedUICards[i].SetSelectIndex(i + 1);
        }
    }

    #endregion

    //打出卡牌
    void PlayCard(UICard uiCard) {
        //触发效果
        uiCard.card.effect.Trigger(GameManager.instance.player, GameManager.instance.enemy);

        //如果无CD，直接移回牌库
        if(uiCard.card.cd == 0) {
            //AddCardToDeck(uiCard.card);
        } else {
            //有CD，移动到CD列表

        }

        //移除卡牌
        RemoveUICard(uiCard);
    }

    #endregion

    #region 玩家手牌

    //创建手牌
    //public void AddCard(Card card) {
    //    //创建UI手牌
    //    UICard uiCard = AddUICard(card);

    //    //实际添加卡牌
    //    cards.Add(card);
    //}

    //手牌洗回卡组
    public void ShuffleCards(int player) {
        List<Card> cards = playerCards[player].cards;
        List<Card> deck = playerCards[player].deck;
        foreach (var card in cards) {
            deck.Add(card);
        }
        cards.Clear();
        Shuffle(ref deck);

        //清除手牌
        ClearUICard(player);
    }


    #endregion

    #region 牌库

    //添加卡牌到牌库
    void AddCardToDeck(int player, Card card) {
        List<Card> deck = playerCards[player].deck;
        deck.Add(card);

        Shuffle(ref deck);
    }

    //洗牌
    void Shuffle(ref List<Card> cards) {
        if (cards.Count <= 1)
            return;

        //循环牌数次，随机调换牌组中的牌
        for (int i = 0; i < cards.Count; i++) {
            int randomIndex = UnityEngine.Random.Range(0, cards.Count);
            Card temp = cards[randomIndex];
            cards[randomIndex] = cards[i];
            cards[i] = temp;
        }
    }

    #endregion

    #region 卡牌墓地

    //墓地
    List<Card> cardGrave = new List<Card>();

    //添加卡牌到墓地
    void AddCardToGrave(Card card) {
        cardGrave.Add(card);

        //设置其当前冷却时间
        card.cdCurrent = card.cd;

        Shuffle(ref cardGrave);
    }

    //更新墓地卡牌冷却
    void GraveUpdate() {
        for (int i = cardGrave.Count - 1; i >= 0; i--) {
            Card card = cardGrave[i];


        }
    }

    #endregion

    //回合开始
    void OnTurnStart() {
        GraveUpdate();
    }

    //让指定玩家抽一张牌
    public void DrawCard(int player) {
        //如果牌库已空


        List<Card> deck = playerCards[player].deck;
        Card card = deck[0];
        deck.RemoveAt(0);

        playerCards[player].cards.Add(card);

        //添加UI卡牌
        AddUICard(player, card);
    }
    public void DrawCard(int player, int num) {
        for (int i = 0; i < num; i++) {
            DrawCard(player);
        }
    }

}

//一个玩家的所有牌
public class PlayerCards {
    //手牌
    public List<Card> cards = new List<Card>();
    //牌库
    public List<Card> deck;
    //墓地
    public List<Card> cardGrave = new List<Card>();
    //UI手牌
    public List<UICard> uiCards = new List<UICard>();

    //初始化牌库，输入一个卡组
    public PlayerCards(Card[] cards) {
        deck = new List<Card>(cards);
        //Shuffle(ref deck);
    }
}

//一个玩家的所有牌
//public class PlayerCards {
//    //手牌
//    List<Card> cards = new List<Card>();
//    //牌库
//    List<Card> deck;
//    //墓地
//    List<Card> cardGrave = new List<Card>();
//    //UI手牌
//    List<UICard> uiCards;

//    //初始化牌库，输入一个卡组
//    public PlayerCards(Card[] cards) {
//        deck = new List<Card>(cards);
//        Shuffle(ref deck);
//    }

//    //从牌库抽牌，可设置对这张牌的额外动作
//    public Card DrawCard(Action<Card> action = null) {
//        //从牌库移除牌
//        Card card = deck[0];
//        deck.RemoveAt(0);

//        //加入手牌
//        cards.Add(card);

//        //触发动作
//        action?.Invoke(card);

//        return card;
//    }

//    public void CreateUICard(Card card) {
//        UICard uiCard = Instantiate(cardPrefab, cardParent).GetComponent<UICard>();
//        uiCard.Set(card);
//    }

//    //打印牌库
//    void PrintDeck() {
//        Debug.Log("------卡组------");

//        foreach (var card in deck) {
//            Debug.Log(card.cardName);
//        }
//    }

//    //洗牌
//    void Shuffle(ref List<Card> cards) {
//        if (cards.Count <= 1)
//            return;

//        //循环牌数次，随机调换牌组中的牌
//        for (int i = 0; i < cards.Count; i++) {
//            int randomIndex = UnityEngine.Random.Range(0, cards.Count);
//            Card temp = cards[randomIndex];
//            cards[randomIndex] = cards[i];
//            cards[i] = temp;
//        }
//    }
//}
