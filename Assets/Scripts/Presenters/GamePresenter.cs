using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class GamePresenter : IInitializable, IDisposable
{
    private readonly ShopView shopView;
    private readonly InventoryView inventoryView;


    private readonly GameView gameView;
    private readonly GameModel gameModel;
    private readonly CompositeDisposable compositeDisposable;

    public GamePresenter(GameView gameView, GameModel gameModel, ShopView shopView, InventoryView inventoryView)
    {
        this.gameView = gameView;
        this.gameModel = gameModel;
        this.shopView = shopView;
        this.inventoryView = inventoryView;
        map = new Dictionary<Vector2Int, GameObject>();
        compositeDisposable = new CompositeDisposable();

    }

    public void Initialize()
    {
        gameModel.Gold.Subscribe(gold => gameView.GoldText = gold);
        gameModel.Date.Subscribe(date => gameView.DateText = date);
        gameModel.Date.Subscribe(date => gameView.TimeText = date);
        gameModel.Date.Subscribe(date => gameView.ClockImage = date);
        gameModel.Season.Subscribe(season => gameView.SeasonText = season);

        gameView.OnShopClick.Subscribe(_ => OnShopShow());
        gameModel.Items.ObserveAdd().Where(p => p.Key.y == 0).Subscribe(p => gameView.SetBottom(p));
        gameModel.Items.ObserveRemove().Where(p => p.Key.y == 0).Subscribe(p => gameView.SetBottom(p));
        gameModel.Items.ObserveReplace().Where(p => p.Key.y == 0).Subscribe(p => gameView.SetBottomChanged(p));


        gameModel.IsUseItem.Subscribe(value => gameView.UseItemMode(value));
        Observable.Interval(new TimeSpan(0, 0, 0, 5, 0)).Subscribe(_ => gameModel.Date.Value = gameModel.Date.Value.AddMinutes(10));
        Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Q)).Subscribe(_ => OnInventoryShow());

        Observable.EveryUpdate()

            .Where(_ => Input.anyKeyDown && Input.inputString.Length >= 1 && '1' <= Input.inputString[0] && Input.inputString[0] <= '9')
            .Select(_ => int.Parse(Input.inputString.Substring(0, 1)) - 1)
            .Where(p => gameModel.Items.ContainsKey(new Vector2Int(p, 0)))
            .Subscribe(value => UseItem(value));


        Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Escape)).Subscribe(_ => gameModel.IsUseItem.Value = false);
        Observable.EveryUpdate().Where(_ => gameModel.IsUseItem.Value).Subscribe(_ => Test());
        Observable.EveryUpdate().Where(_ => gameModel.IsUseItem.Value).Where(_=>Input.GetMouseButtonDown(0)).Subscribe(_ => SetCrop());

    }
    private int number;
    private void Test()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = new Vector2Int(Mathf.FloorToInt(mouse.x + 0.5f), Mathf.FloorToInt(mouse.y - 0.5f));
        var vector3 = new Vector3(pos.x + 0.5f, pos.y + 0.5f);
        gameView.Box.position = vector3;
        gameView.Box.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
    }

    public Dictionary<Vector2Int, GameObject> map;

    private void SetCrop()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = new Vector2(Mathf.FloorToInt(mouse.x + 0.5f) + 0.5f, Mathf.FloorToInt(mouse.y - 0.5f));
        var key = new Vector2Int(Mathf.FloorToInt(mouse.x + 0.5f) , Mathf.FloorToInt(mouse.y - 0.5f));
        if(!map.ContainsKey(key))
        {
            Vector2Int vector2Int = new Vector2Int(number, 0);
            var item = gameModel.Items[vector2Int];
            var crop = GameObject.Instantiate(Resources.Load<GameObject>("Crop"), new Vector3(pos.x, pos.y),Quaternion.identity);
            var id = gameModel.ItemObjects[item.ID].CropID;
            crop.GetComponent<Crop>().SetData(id);
            map.Add(key, crop);
            if (item.Count > 1)
            {
                item.Count -= 1;
                gameModel.Items[vector2Int] = item;
            }
            else
            {
                gameModel.Items.Remove(vector2Int);
                gameModel.IsUseItem.Value = false;
            }

        }

        Debug.Log(number);

    }
    private void UseItem(int index)
    {
        number = index;
        gameModel.IsUseItem.Value = true;
    }

    private void OnShopShow()
    {
        shopView.gameObject.SetActive(true);
    }
    private void OnInventoryShow()
    {
        inventoryView.gameObject.SetActive(true);
    }
    public void NextDay()
    {
        gameModel.NextDay();
    }

    public void Dispose()
    {
        compositeDisposable.Dispose();
    }
}
