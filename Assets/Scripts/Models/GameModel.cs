using System;
using UniRx;
using Zenject;

public class GameModel
{
    public ReactiveProperty<int> Gold { get; set; }
    public ReactiveProperty<DateTime> Date { get; set; }
    public ReadOnlyReactiveProperty<Season> Season { get; set; }

    public GameModel()
    {
        Gold = new ReactiveProperty<int>(0);
        Date = new ReactiveProperty<DateTime>(new DateTime(100, 1, 1));
        Season = Date.Select(value => (Season)((value.Month - 1) % 4)).ToReadOnlyReactiveProperty();
    }

    public void NextDay()
    {

        var nextDay = Date.Value.AddDays(1);
        Date.Value = nextDay;

    }


}
