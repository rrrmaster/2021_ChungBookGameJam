using System;
using UniRx;
using UnityEngine;
public class GamePresenter : MonoBehaviour
{
    public GameView gameView;
    public GameModel gameModel;

    public void Awake()
    {
        gameModel = new GameModel();

        gameModel.Gold.Subscribe(gold => gameView.GoldText = gold);
        gameModel.Date.Subscribe(date => gameView.DateText = date);
    }

    public void NextDay()
    {
        gameModel.NextDay();
    }
}
