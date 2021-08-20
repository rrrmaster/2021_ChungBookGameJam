using Zenject;
using UnityEngine;

public class ShopPresenter
{
    [Inject]
    public ShopView shopView;

    [Inject]
    public ShopModel shopModel;

    public ShopPresenter()
    {

    }
}
