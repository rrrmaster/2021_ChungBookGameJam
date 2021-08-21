using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopView : MonoBehaviour
{
    [SerializeField]
    private Button shopCloseButton;

    [SerializeField]
    private GameObject shopItem;
    [SerializeField]
    private Transform shopListContent;

    [Inject]
    private GameModel gameModel;


    public IObservable<Unit> OnShopCloseClick
    {
        get => shopCloseButton.OnClickAsObservable();
    }

    public void AddShopItem(ShopItemModel value)
    {
        var item = Instantiate(shopItem, shopListContent).GetComponent<ShopItemView>();
        item.OnShopBuy.Subscribe(_ => Buy(value));
        item.Name = value.Name;
        item.Icon = value.Icon;
        item.Price = value.Price.ToString();
    }
    private void Buy(ShopItemModel value)
    {
        if (gameModel.Gold.Value >= value.Price)
        {
            gameModel.Gold.Value -= value.Price;
            gameModel.AddItem(new Item() { ID = value.ID, Count = 1 });

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (gameModel.Items.ContainsKey(new Vector2Int(x, y)))
                        Debug.Log($"{gameModel.Items[new Vector2Int(x, y)].ID} : {gameModel.Items[new Vector2Int(x, y)].Count}");
                }
            }
        }
    }
}

