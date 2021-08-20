using System;
using UniRx;
using UnityEngine;
using Zenject;

public class GamePresenter : IInitializable, IDisposable
{
    private readonly GameView gameView;
    private readonly GameModel gameModel;
    
    private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

    public GamePresenter(GameView gameView, GameModel gameModel)
    {
        this.gameView = gameView;
        this.gameModel = gameModel;
    }

    public void Initialize()
    {
        gameModel.Gold.Subscribe(gold => gameView.GoldText = gold);
        gameModel.Date.Subscribe(date => gameView.DateText = date);
        gameModel.Season.Subscribe(season => gameView.SeasonText = season);
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
