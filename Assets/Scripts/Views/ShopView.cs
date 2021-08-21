using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    [SerializeField]
    private Transform shopInventory;

    [Inject]
    private GameModel gameModel;

    private Dictionary<int, ShopItemView> shopItemModels = new Dictionary<int, ShopItemView>();

    public IObservable<Unit> OnShopCloseClick
    {
        get => shopCloseButton.OnClickAsObservable();
    }

    private void Awake()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                Transform transform1 = shopInventory.GetChild((2 - y) * 9 + x);
                transform1.GetComponent<ShopSellView>().pos = new Vector2Int(x, y);
            }
        }
    }
    public void AddShopItem(StockItemModel value)
    {
        var item = Instantiate(shopItem, shopListContent).GetComponent<ShopItemView>();
        shopItemModels.Add(value.ID, item);
        item.OnShopBuy.Subscribe(_ => Buy(value));
        item.Name = value.Name;
        item.Icon = value.Icon;
        item.SetPrice(value.Price, value.OldPrice);
    }

    internal void ChangeShopItem(CollectionReplaceEvent<StockItemModel> p)
    {
        shopItemModels[p.NewValue.ID].SetPrice(p.NewValue.Price, p.NewValue.OldPrice);
    }

    private void Buy(StockItemModel value)
    {
        var item = gameModel.StockItems.FirstOrDefault(p => p.ID == value.ID);
        if (gameModel.Gold.Value >= item.Price)
        {
            gameModel.Gold.Value -= item.Price;
            gameModel.AddItem(new Item() { ID = value.ID, Count = 1 });

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (gameModel.Items.ContainsKey(new Vector2Int(x, y)))
                        Debug.Log($"{gameModel.Items[new Vector2Int(x, y)].ID} : {gameModel.Items[new Vector2Int(x, y)].Count}");
                }
            }
        }
    }
    internal void SetBottom(DictionaryAddEvent<Vector2Int, Item> p)
    {
        Transform transform1 = shopInventory.GetChild(p.Key.x + (2 - p.Key.y) * 9).GetChild(0);
        transform1.gameObject.SetActive(true);
        transform1.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<ItemObject>("Items").FirstOrDefault(v => v.ID == p.Value.ID).Icon;
        transform1.GetComponentInChildren<TextMeshProUGUI>().text = p.Value.Count.ToString();
    }

    internal void SetInventoryItemRemoved(DictionaryRemoveEvent<Vector2Int, Item> p)
    {
        Transform transform1 = shopInventory.GetChild(p.Key.x + (2 - p.Key.y) * 9).GetChild(0);
        transform1.gameObject.SetActive(false);
    }

    internal void SetInventoryItemChanged(DictionaryReplaceEvent<Vector2Int, Item> p)
    {
        Transform transform1 = shopInventory.GetChild(p.Key.x + (2 - p.Key.y) * 9).GetChild(0);
        transform1.gameObject.SetActive(true);
        transform1.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<ItemObject>("Items").FirstOrDefault(v => v.ID == p.NewValue.ID).Icon;
        transform1.GetComponentInChildren<TextMeshProUGUI>().text = p.NewValue.Count.ToString();
    }
}

