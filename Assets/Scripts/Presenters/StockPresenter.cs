using UniRx;
using UnityEngine;

public class StockPresenter : MonoBehaviour
{
    
}


public class StockModel
{
    public ReactiveCollection<StockItem> StockItems;

    public StockModel()
    {
        StockItems = new ReactiveCollection<StockItem>();
    }
}

public class StockItem
{
    public int ID;
    public int Price;
}