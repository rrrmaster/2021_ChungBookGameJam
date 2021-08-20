using UniRx;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopModel : IInitializable
{
    public ReactiveCollection<ShopItem> ShopItems;

    public void Initialize()
    {
        ShopItems = new ReactiveCollection<ShopItem>();

        var crops = Resources.LoadAll<CropObject>("Crops");
        Debug.Log(crops.Length);
        foreach (var item in crops)
        {
            Debug.Log(item.Name);
        }
    }
}


public struct ShopItem
{

}