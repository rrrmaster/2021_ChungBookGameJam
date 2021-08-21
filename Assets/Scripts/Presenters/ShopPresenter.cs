using System;
using UniRx;
using UnityEngine;
using Zenject;
public class ShopPresenter : IInitializable, IDisposable
{
    private readonly ShopView shopView;
    private readonly ShopModel shopModel;


    private readonly CompositeDisposable compositeDisposable ;

    public ShopPresenter(ShopView shopView, ShopModel shopModel)
    {
        this.shopView = shopView;
        this.shopModel = shopModel;

        compositeDisposable = new CompositeDisposable();
    }
    public void Initialize()
    {
        shopModel.ShopItems.ObserveAdd().Subscribe(p => shopView.AddShopItem(p.Value));
        shopView.OnShopCloseClick.Subscribe(_ => shopView.gameObject.SetActive(false));
        var crops = Resources.LoadAll<CropObject>("Crops");


        foreach (var crop in crops)
        {
            ShopItemModel item = new ShopItemModel() { Name = crop.Name,Icon = crop.Icon,ID = crop.ItemID };
            shopModel.ShopItems.Add(item);

        }
    }
    public void Dispose()
    {
        compositeDisposable.Dispose();
    }

  
}
