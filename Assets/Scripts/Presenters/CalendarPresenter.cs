using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class CalendarPresenter : IInitializable, IDisposable
{
    private readonly CalendarView calendarView;
    private readonly GameModel gameModel;
    private readonly CompositeDisposable compositeDisposable;

    public CalendarPresenter(CalendarView calendarView, GameModel gameModel)
    {
        this.calendarView = calendarView;
        this.gameModel = gameModel;
        compositeDisposable = new CompositeDisposable();
    }
    public void Initialize()
    {
        calendarView.closeButton.OnClickAsObservable().Subscribe(_=>calendarView.gameObject.SetActive(false));
        gameModel.Date.Subscribe(p=>calendarView.SetCalendar(p));
        gameModel.Date.Subscribe(p => calendarView.dayText.text = p.ToString("yyy³â MM¿ù"));
        gameModel.Season.Subscribe(p => calendarView.calendarImage.sprite = calendarView.calendarSeason[(int)p]);
    }
    public void Dispose()
    {
        compositeDisposable.Dispose();
    }

  
}
