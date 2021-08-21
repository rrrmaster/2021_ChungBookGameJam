using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;

public class MainPresenter: IInitializable
{
    [Inject]
    private MainView mainView;

    [Inject]
    private OptionView optionView;


    public void Initialize()
    {
        mainView.StartButton.OnClickAsObservable().Subscribe(_ => OnClickGameStartButton());
        mainView.OptionButton.OnClickAsObservable().Subscribe(_ => OnClickOptionButton());
        mainView.ExitButton.OnClickAsObservable().Subscribe(_ => OnClickExitButton());

        optionView.CloseButton.OnClickAsObservable().Subscribe(_ => OnClickOptionViewCloseButton());
    }
    private  void OnClickGameStartButton()
    {
        SceneManager.LoadScene("Game");
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
