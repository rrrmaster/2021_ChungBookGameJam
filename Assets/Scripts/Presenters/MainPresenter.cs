using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MainPresenter : MonoBehaviour
{
    [SerializeField] private MainView mainView;

    private void Start()
    {
        mainView.StartButton.OnClickAsObservable().Subscribe(_ => OnClickGameStartButton());
        mainView.OptionButton.OnClickAsObservable().Subscribe(_ => OnClickOptionButton());
        mainView.ExitButton.OnClickAsObservable().Subscribe(_ => OnClickExitButton());
    }

    private void OnClickGameStartButton()
    {

    }

    private void OnClickOptionButton()
    {

    }

    private void OnClickExitButton()
    {
        Application.Quit();
    }
}
