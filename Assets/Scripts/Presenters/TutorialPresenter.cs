using System;
using UniRx;
using UnityEngine;
using Zenject;

public class TutorialPresenter : IInitializable, IDisposable
{
    [Inject]
    private TutorialView tutorialView;

    private ReactiveProperty<int> index;

    IDisposable observable;
    void IInitializable.Initialize()
    {
        Time.timeScale = 0;
        index = new ReactiveProperty<int>(0);
        observable = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ => Next());
        index.Subscribe(value => tutorialView.SetActive(value));
    }
    private void Next()
    {
        index.Value += 1;
        if (index.Value == tutorialView.TutorialChild.childCount)
        {
            Time.timeScale = 1;
            tutorialView.gameObject.SetActive(false);
            observable.Dispose();
        }
    }
    void IDisposable.Dispose()
    {
    }

}