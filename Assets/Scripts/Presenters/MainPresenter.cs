using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPresenter : MonoBehaviour
{
    [SerializeField] private MainView mainView;
    [SerializeField] private OptionView optionView;

    private void Start()
    {
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

        optionView.Sliders.ForEach((x) =>
        x.onValueChanged.AddListener((value) =>
        {
            x.value = value;
            AdjustVolumes();
        }));

        SoundManager.Instance.PlayBGMSound("BGMSample");
    }

    private void AdjustVolumes()
    {
        SoundManager.Instance.AdjustMasterVolume(optionView.MasterSlider.value);
        SoundManager.Instance.AdjustBGMVolume(optionView.BgmSlider.value);
        SoundManager.Instance.AdjustFxVoulme(optionView.FxSlider.value);
    }


    private void OnClickGameStartButton()
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
