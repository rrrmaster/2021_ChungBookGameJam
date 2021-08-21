using System;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ShopPresenter : IInitializable, IDisposable
{
    private readonly ShopView shopView;
    private readonly ShopModel shopModel;
    private readonly GameModel gameModel;


    private readonly CompositeDisposable compositeDisposable;

    public ShopPresenter(ShopView shopView, ShopModel shopModel, GameModel gameModel)
    {
        this.shopView = shopView;
        this.shopModel = shopModel;
        this.gameModel = gameModel;

        compositeDisposable = new CompositeDisposable();
    }
    public void Initialize()
    {
        gameModel.Items.ObserveAdd().Subscribe(v => shopView.SetBottom(v));
        gameModel.Items.ObserveRemove().Where(p => p.Key.y == 0).Subscribe(p => shopView.SetInventoryItemRemoved(p));
        gameModel.Items.ObserveReplace().Subscribe(v => shopView.SetInventoryItemChanged(v));

        gameModel.StockItems.ObserveAdd().Subscribe(p => shopView.AddShopItem(p.Value));
        gameModel.StockItems.ObserveReplace().Subscribe(p => shopView.ChangeShopItem(p));
        shopView.OnShopCloseClick.Subscribe(_ => shopView.gameObject.SetActive(false));

        var crops = Resources.LoadAll<CropObject>("Crops");
        foreach (var crop in crops)
        {
            StockItemModel item = new StockItemModel() {
                Name = crop.Name,
                Icon = crop.Icon,
                ID = crop.ItemID,
                De = crop.Description,
                Price = crop.BasePrice, 
                OldPrice = crop.BasePrice,
                SellID = crop.SellItemID
            };
            gameModel.StockItems.Add(item);

        }
    }
    public void Dispose()
    {
        compositeDisposable.Dispose();
    }


}
