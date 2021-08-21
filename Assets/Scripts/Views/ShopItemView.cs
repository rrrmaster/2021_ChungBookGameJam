using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Globalization;

public class ShopItemView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI NameText;

    [SerializeField]
    private TextMeshProUGUI PriceText;

    [SerializeField]
    private Image IconImage;

    [SerializeField]
    private Button buyButton;

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
