using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOptionPrensenter : IInitializable
{
    [Inject]
    private GameOptionView optionView;


    public void Initialize()
    {
        SoundManager.Instance.PlayBGMSound("MainTitle");
        optionView.CloseButton.OnClickAsObservable().Subscribe(_ => OnClickCloseButton());
        optionView.ExitButton.OnClickAsObservable().Subscribe(_ => OnClickExitButton());



        optionView.Sliders = new List<Slider>
        {
            optionView.MasterSlider,
            optionView.BgmSlider,
            optionView.FxSlider
        };
        optionView.Sliders[0].value = SoundManager.Instance.MasterVoulme;
        optionView.Sliders[1].value = SoundManager.Instance.BGMVolume;
        optionView.Sliders[2].value = SoundManager.Instance.FxVoulme;
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

    private void OnClickCloseButton()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        optionView.gameObject.SetActive(false);
    }

    private void OnClickExitButton()
    {
        SoundManager.Instance.PlayFXSound("Popup");
        Application.Quit();
    }

}
