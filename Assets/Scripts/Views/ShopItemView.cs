using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using System;
using System.Globalization;
using Zenject;

public class ShopItemView : MonoBehaviour, IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI NameText;

    [SerializeField]
    private TextMeshProUGUI PriceText;

    [SerializeField]
    private Image IconImage;

    [SerializeField]
    private Button buyButton;


    [Inject]
    private ToolTip toolTip;
    public string Name
    {
        set => NameText.text = value;
    }

    public Sprite Icon
    {
        set => IconImage.sprite = value;
    }
    public IObservable<Unit> OnShopBuy
    {
        get => buyButton.OnClickAsObservable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<ToolTip>(true).Show();
    }
    public StockItemModel stockItemModel;
    public void OnPointerExit(PointerEventData eventData)
    {

        FindObjectOfType<ToolTip>(true).Hide();
    }
    public void OnPointerMove(PointerEventData eventData)
    {

        ToolTip toolTip1 = FindObjectOfType<ToolTip>(true);
        toolTip1.SetPosition(eventData.position);
        var seasons = stockItemModel.Seasons;
        var v = new Dictionary<Season, string>() {
                { Season.Spring, "<color=#F17C69>봄</color>"},
                { Season.Summer, "<color=#36A803>여름</color>"},
                { Season.Autumn, "<color=#BF1D10>가을</color>"},
                { Season.Winter, "<color=#45F8FF>겨울</color>"},
            }[seasons];
        var de = $"계절 : {v}\n수확 시기 : {stockItemModel.GrowDay}일";
        toolTip1.SetText(stockItemModel.Name, de);
    }

    internal void SetPrice(int price, int oldPrice)
    {
        if (oldPrice < price)
            PriceText.text = $"<color=#FF5B5D>{price:N0}G <sprite=0/> <size=10>{ (price / (double)oldPrice - 1).ToString("P", CultureInfo.InvariantCulture)}</size></color>";
        else if (oldPrice > price)
            PriceText.text = $"<color=#60CEE4>{price:N0}G <sprite=1/> <size=10>{(1 - price / (double)oldPrice).ToString("P", CultureInfo.InvariantCulture)}</size></color>";
        else
            PriceText.text = $"<color=#ffffff>{price:N0}G <size=10>00.00%</size></color>";

    }
}
