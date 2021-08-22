using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using static GameInstaller;

public class GamePresenter : IInitializable, IDisposable
{
    private readonly ShopView shopView;
    private readonly InventoryView inventoryView;


    private readonly GameView gameView;
    private readonly GameModel gameModel;
    private readonly CompositeDisposable compositeDisposable;
    private readonly GameOptionView gameOptionView;
    private readonly Setting setting;
    public GamePresenter(GameView gameView, GameModel gameModel, ShopView shopView, InventoryView inventoryView,GameOptionView gameOptionView,Setting setting)
    {
        this.gameView = gameView;
        this.gameModel = gameModel;
        this.shopView = shopView;
        this.inventoryView = inventoryView;
        this.gameOptionView = gameOptionView;
        this.setting = setting;
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
        gameModel.Season.Subscribe(season => gameView.SeasonPanel = season);
        gameModel.Health.Subscribe(health => gameView.SetHealth(health));

        gameView.OnShopClick.Subscribe(_ => OnShopShow());
        gameView.OnDungeonClick.Subscribe(_ => OnDungeonShow());
        gameView.OnOptionClick.Subscribe(_ => OnOptionShow());

        gameModel.Items.ObserveAdd().Where(p => p.Key.y == 0).Subscribe(p => gameView.SetBottom(p));
        gameModel.Items.ObserveRemove().Where(p => p.Key.y == 0).Subscribe(p => gameView.SetBottom(p));
        gameModel.Items.ObserveReplace().Where(p => p.Key.y == 0).Subscribe(p => gameView.SetBottomChanged(p));


        gameModel.IsUseItem.Subscribe(value => gameView.UseItemMode(value));
        Observable.Interval(new TimeSpan(0, 0, 0, 5, 0)).Subscribe(_ => AddTime());
        Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.E)).Subscribe(_ => OnInventoryShow());
        Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.U)).Subscribe(_ => NextDay());

        Observable.EveryUpdate()
            .Where(_ => Input.anyKeyDown && Input.inputString.Length >= 1 && '1' <= Input.inputString[0] && Input.inputString[0] <= '9')
            .Select(_ => int.Parse(Input.inputString.Substring(0, 1)) - 1)
            .Where(p => gameModel.Items.ContainsKey(new Vector2Int(p, 0)))
            .Subscribe(value => UseItem(value));


        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ => Test1());
        Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Escape)).Subscribe(_ => gameModel.IsUseItem.Value = false);
        Observable.EveryUpdate().Where(_ => gameModel.IsUseItem.Value).Subscribe(_ => Test());
        Observable.EveryUpdate().Where(_ => gameModel.IsUseItem.Value).Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ => SetCrop());

    }

    internal void Washing(Vector2Int nKey)
    {
        setting.TileMap.SetTile(new Vector3Int(nKey.x, nKey.y,0), setting.washingTile);
    }

    public Dictionary<Vector2Int, GameObject> map;

    private int number;
    private void AddTime()
    {
        DateTime value = gameModel.Date.Value;
        DateTime dateTime = value.AddMinutes(10);
        if (value.Date == dateTime.Date)
            gameModel.Date.Value = dateTime;
        else
            NextDay();
    }
    private void Test()
    {
        Vector2Int vector2Int = new Vector2Int(number, 0);
        var item = gameModel.Items[vector2Int];
        var id = gameModel.ItemObjects.FirstOrDefault(p => p.ID == item.ID).CropID;
        var a = gameModel.CropObjects.FirstOrDefault(p => p.ID == id);

        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = new Vector2Int(Mathf.FloorToInt(mouse.x + 0.5f), Mathf.FloorToInt(mouse.y - 0.5f));
        var key = new Vector2Int(Mathf.FloorToInt(mouse.x + 0.5f), Mathf.FloorToInt(mouse.y - 0.5f));
        var vector3 = new Vector3(pos.x + 0.5f, pos.y + 0.5f);
        gameView.Box.position = vector3;
        if (!map.ContainsKey(key) && setting.isCropTileMap.HasTile(new Vector3Int(key.x, key.y, 0)) && (a.Seasons | gameModel.Season.Value) == gameModel.Season.Value)
            gameView.Box.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
        else
            gameView.Box.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
    }
    private void Test1()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var key = new Vector2Int(Mathf.FloorToInt(mouse.x + 0.5f), Mathf.FloorToInt(mouse.y - 0.5f));
        if (map.ContainsKey(key))
        {
            var crop = map[key].GetComponent<Crop>();
            if (crop.Grow.Value >= crop.maxGrow)
            {

                SoundManager.Instance.PlayFXSound("Harvesting");
                var dropItems = Resources.LoadAll<CropObject>("Crops").FirstOrDefault(p => p.ID == crop.id);
                foreach (var item in dropItems.ItemObjects)
                {
                    gameModel.AddItem(new Item { Count = 1, DateTime = gameModel.Date.Value.Date, ID = item.ID });

                }
                GameObject.Destroy(map[key].gameObject);
                map.Remove(key);
            }
        }
    }

    private void SetCrop()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = new Vector2(Mathf.FloorToInt(mouse.x + 0.5f) + 0.5f, Mathf.FloorToInt(mouse.y - 0.5f));
        var key = new Vector2Int(Mathf.FloorToInt(mouse.x + 0.5f), Mathf.FloorToInt(mouse.y - 0.5f));
        if (!map.ContainsKey(key) && setting.isCropTileMap.HasTile(new Vector3Int(key.x, key.y,0)))
        {
            Vector2Int vector2Int = new Vector2Int(number, 0);
            var item = gameModel.Items[vector2Int];
            var id = gameModel.ItemObjects.FirstOrDefault(p => p.ID == item.ID).CropID;
            var a = Resources.LoadAll<CropObject>("Crops").FirstOrDefault(p => p.ID == id);
            if ((gameModel.Season.Value & a.Seasons) == gameModel.Season.Value)
            {

                var crop = GameObject.Instantiate(Resources.Load<GameObject>("Crop"), new Vector3(pos.x, pos.y), Quaternion.identity);

                SoundManager.Instance.PlayFXSound("Seed");
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

        }

        Debug.Log(number);

    }
    private void UseItem(int index)
    {

        var use = gameModel.ItemObjects.FirstOrDefault(p => p.ID == gameModel.Items[new Vector2Int(index, 0)].ID);
        if (use && use.IsCrop)
        {

            number = index;
            gameModel.IsUseItem.Value = true;
        }
    }
    private void OnShopShow()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        shopView.gameObject.SetActive(true);
    }
    private void OnDungeonShow()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        GameObject.FindObjectOfType<DungeonManager>().OnClickDungeonButton();
    }
    private void OnOptionShow()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        gameOptionView.gameObject.SetActive(true);  
    }
    private void OnInventoryShow()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        inventoryView.gameObject.SetActive(true);
    }
    public void NextDay()
    {
        NewMethod();
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(133, 12);
        gameView.NextDay(() => NewMethod());
    }

    private void NewMethod()
    {
        gameModel.Health.Value = 5;
        foreach (var item in map)
        {
            Crop crop = item.Value.GetComponent<Crop>();
            if (crop.IsTodayWashing)
            {
                crop.Grow.Value += 1;
            }
            setting.TileMap.SetTile(new Vector3Int(item.Key.x, item.Key.y, 0), setting.noWashingTile);
            crop.IsTodayWashing = false;
        }
        for (int i = 0; i < gameModel.StockItems.Count; i++)
        {
            var value = gameModel.StockItems[i];
            value.OldPrice = value.Price;
            value.Price = value.Price + Mathf.RoundToInt(value.Price * UnityEngine.Random.Range(-0.15f, 0.20f));
            gameModel.StockItems[i] = value;
        }
        gameModel.NextDay();
        foreach (var item in map)
        {
            Crop crop = item.Value.GetComponent<Crop>();
            var cropInfo = gameModel.CropObjects.First(p => crop.id == p.ID);

            if ((cropInfo.Seasons|gameModel.Season.Value) == gameModel.Season.Value || (cropInfo.HarvestSeasons | gameModel.Season.Value) == gameModel.Season.Value)
                continue;
            GameObject.Destroy(item.Value);
            map.Remove(item.Key);
        }


    }

    public void Dispose()
    {
        compositeDisposable.Dispose();
    }
}
