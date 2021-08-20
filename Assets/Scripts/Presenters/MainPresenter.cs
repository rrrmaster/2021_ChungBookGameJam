using UnityEngine;
using UniRx;
using System.Collections.Generic;

public class MainPresenter : MonoBehaviour
{
    [SerializeField] private MainView mainView;
    [SerializeField] private OptionView optionView;
    public InGameView inGameView;

    private void Start()
    {
        mainView.StartButton.OnClickAsObservable().Subscribe(_ => OnClickGameStartButton());
        mainView.OptionButton.OnClickAsObservable().Subscribe(_ => OnClickOptionButton());
        mainView.ExitButton.OnClickAsObservable().Subscribe(_ => OnClickExitButton());

        optionView.CloseButton.OnClickAsObservable().Subscribe(_ => OnClickOptionViewCloseButton());
    }

    private void OnClickGameStartButton()
    {

    }

    private void OnClickOptionButton()
    {
        optionView.gameObject.SetActive(true);
    }

    private void OnClickExitButton()
    {
        Application.Quit();
    }

    private void OnClickOptionViewCloseButton()
    {
        optionView.gameObject.SetActive(false);
    }

}
