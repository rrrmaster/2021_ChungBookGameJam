using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OptionView : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider fxSlider;


    private List<Slider> sliders;

    public Button CloseButton
    {
        get => closeButton;
    }

    public List<Slider> Sliders { get => sliders; set => sliders = value; }

    public Slider FxSlider { get => fxSlider; set => fxSlider = value; }
    public Slider BgmSlider { get => bgmSlider; set => bgmSlider = value; }
    public Slider MasterSlider { get => masterSlider; set => masterSlider = value; }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}

