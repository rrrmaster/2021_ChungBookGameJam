using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class GameModel
{
    public ReactiveProperty<int> Gold { get; set; }
    public ReactiveProperty<bool> IsUseItem{ get; set; }
    public ReactiveProperty<DateTime> Date { get; set; }
    public ReadOnlyReactiveProperty<Season> Season { get; set; }
    public ReactiveDictionary<Vector2Int, Item> Items { get; set; }

    public ReactiveCollection<StockItemModel> StockItems;

    public readonly ItemObject[] ItemObjects;
    public GameModel()
    {
        IsUseItem = new ReactiveProperty<bool>(false);
        Items = new ReactiveDictionary<Vector2Int, Item>();
        StockItems = new ReactiveCollection<StockItemModel>();
           Gold = new ReactiveProperty<int>(1000);
        Date = new ReactiveProperty<DateTime>(new DateTime(100, 1, 1,6,0,0));
        ItemObjects = Resources.LoadAll<ItemObject>("Items");

        var v = new Season[4] {
        global::Season.Spring,
        global::Season.Summer,
        global::Season.Autumn,
        global::Season.Winter
        };
        Season = Date.Select(value => v[(value.Month - 1) % 4]).ToReadOnlyReactiveProperty();
    }

    public void NextDay()
    {

        var nextDay = Date.Value.AddDays(1).Date;
        nextDay = nextDay.AddHours(6);

        Date.Value = nextDay;

    }

    internal void AddItem(Item item)
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (Items.ContainsKey(new Vector2Int(x, y)))
                {
                    var i = Items[new Vector2Int(x, y)];
                    if(i.ID == item.ID && i.DateTime == item.DateTime)
                    {
                        i.Count += 1;
                        Items[new Vector2Int(x, y)] = i;
                        return;
                    }
                }
            }
        }
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if(!Items.ContainsKey(new Vector2Int(x,y)))
                {
                    Items.Add(new Vector2Int(x, y), item);
                    return;
                }
            }   
        }

    }
}



public struct StockItemModel
{
    public int Price { get; set; }
    public string Name { get; set; }
    public string De { get; set; }
    public Sprite Icon { get; set; }
    public int ID { get; internal set; }
    public int OldPrice { get; internal set; }
    public int SellID { get; internal set; }
}
[Serializable]
public struct Item
{
    public int ID { get; set; }
    public int Count { get; set; }
    public DateTime DateTime { get; set; }

}