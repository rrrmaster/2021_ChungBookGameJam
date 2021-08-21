using UniRx;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopModel
{
    public ReactiveCollection<ShopItemModel> ShopItems;

    public ShopModel()
    {
        ShopItems = new ReactiveCollection<ShopItemModel>();

    
    }
}


public struct ShopItemModel
{
    public int Price { get; set; }
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public int ID { get; internal set; }
}