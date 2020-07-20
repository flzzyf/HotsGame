using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICard : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler {
    Image image;
    Outline outline;

    //UI组件
    public Image icon;
    public Image cardTypeIcon;
    public Text text_Title;
    public Text text_Info;
    public Text text_CD;
    public GameObject panel_Selected;
    public Text text_SelectedIndex;

    //卡
    public Card card;

    void Start() {
        image = GetComponent<Image>();

        outline = GetComponent<Outline>();
        outline.DOFade(0, 0);
    }

    #region UI交互事件

    public event Action onClick;

    //被点击
    public void OnPointerDown(PointerEventData eventData) {
        onClick?.Invoke();
    }

    int currentSibling;

    public void OnPointerEnter(PointerEventData eventData) {
        outline.DOFade(1, .3f);

        //currentSibling = transform.GetSiblingIndex();
        //transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData) {
        outline.DOFade(0, .3f);

        //transform.SetSiblingIndex(currentSibling);
    }

    #endregion

    public void Set(Card card) {
        this.card = card;
        icon.sprite = card.icon;
        text_Title.text = card.cardName;
        text_Info.text = card.info;
        text_CD.text = card.cd.ToString();
        cardTypeIcon.sprite = StaticData.instance.GetCardTypeIcon(card.type);
    }

    void Clear() {
        onClick = null;
    }

    #region 选取

    [NonSerialized]
    public bool isSelected;

    //选取
    public void Select(int index) {
        panel_Selected.SetActive(true);

        isSelected = true;

        SetSelectIndex(index);
    }
    public void Deselect() {
        isSelected = false;

        panel_Selected.SetActive(false);
    }

    //设置选取序号
    public void SetSelectIndex(int index) {
        text_SelectedIndex.text = index.ToString();
    }

    #endregion

    void OnValidate() {
        if(card != null)
            Set(card);
    }
}
