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

    [SerializeField] private Text masterText;
    [SerializeField] private Text bgmText;
    [SerializeField] private Text fxText;


    private List<Slider> sliders;

    public Button CloseButton
    {
        get => closeButton;
    }

    public List<Slider> Sliders { get => sliders; set => sliders = value; }

    public Slider FxSlider { get => fxSlider; set => fxSlider = value; }
    public Slider BgmSlider { get => bgmSlider; set => bgmSlider = value; }
    public Slider MasterSlider { get => masterSlider; set => masterSlider = value; }
    public Text MasterText { get => masterText; set => masterText = value; }
    public Text BgmText { get => bgmText; set => bgmText = value; }
    public Text FxText { get => fxText; set => fxText = value; }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}

