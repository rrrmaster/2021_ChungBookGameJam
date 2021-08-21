using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

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
    public string Price
    {
        set => PriceText.text = value;
    }
    public Sprite Icon
    {
        set => IconImage.sprite = value;
    }
    public IObservable<Unit> OnShopBuy
    {
        get => buyButton.OnClickAsObservable();
    }
}
