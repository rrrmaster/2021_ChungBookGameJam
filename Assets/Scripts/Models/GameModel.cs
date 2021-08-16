using System;
using UniRx;

public class GameModel
{
    public ReactiveProperty<int> Gold { get; set; }
    public ReactiveProperty<DateTime> Date { get; set; }

    public GameModel()
    {
        Gold = new ReactiveProperty<int>(0);
        Date = new ReactiveProperty<DateTime>(new DateTime(100, 1, 1));
    }

    public void NextDay()
    {
        Date.Value = Date.Value.AddDays(1);

    }
}
