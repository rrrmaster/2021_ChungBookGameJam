using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPresenter : IInitializable
{
    [Inject]
    private MainView mainView;

    [Inject]
    private OptionView optionView;


    public void Initialize()
    {
        SoundManager.Instance.PlayBGMSound("MainTitle");
        mainView.StartButton.OnClickAsObservable().Subscribe(_ => OnClickGameStartButton());
        mainView.OptionButton.OnClickAsObservable().Subscribe(_ => OnClickOptionButton());
        mainView.ExitButton.OnClickAsObservable().Subscribe(_ => OnClickExitButton());

        optionView.CloseButton.OnClickAsObservable().Subscribe(_ => OnClickOptionViewCloseButton());



        optionView.Sliders = new List<Slider>
        {
            optionView.MasterSlider,
            optionView.BgmSlider,
            optionView.FxSlider
        };
        optionView.Sliders.ForEach(slider => slider.value = 0.5f);
        optionView.Sliders.ForEach((x) =>
        x.OnValueChangedAsObservable().Subscribe((value) => {
            x.value = value;
            AdjustVolumes();
        }));
    }
    private void AdjustVolumes()
    {
        SoundManager.Instance.AdjustMasterVolume(optionView.MasterSlider.value);
        SoundManager.Instance.AdjustBGMVolume(optionView.BgmSlider.value);
        SoundManager.Instance.AdjustFxVoulme(optionView.FxSlider.value);

        optionView.MasterText.text = (SoundManager.Instance.MasterVoulme * 100).ToString("F0");
        optionView.BgmText.text = (SoundManager.Instance.BGMVolume * 100).ToString("F0");
        optionView.FxText.text = (SoundManager.Instance.FxVoulme * 100).ToString("F0");
    }

    private void OnClickGameStartButton()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        mainView.Dimmad(() => SceneManager.LoadScene("Game"));
    }

    private void OnClickOptionButton()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        optionView.gameObject.SetActive(true);
    }

    private void OnClickExitButton()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        Application.Quit();
    }

    private void OnClickOptionViewCloseButton()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        optionView.gameObject.SetActive(false);
    }


}
