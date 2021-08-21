using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class OptionView : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider fxSlider;

    [SerializeField] private TextMeshProUGUI masterText;
    [SerializeField] private TextMeshProUGUI bgmText;
    [SerializeField] private TextMeshProUGUI fxText;


    private List<Slider> sliders;

    public Button CloseButton
    {
        get => closeButton;
    }

    public List<Slider> Sliders { get => sliders; set => sliders = value; }

    public Slider FxSlider { get => fxSlider; set => fxSlider = value; }
    public Slider BgmSlider { get => bgmSlider; set => bgmSlider = value; }
    public Slider MasterSlider { get => masterSlider; set => masterSlider = value; }
    public TextMeshProUGUI MasterText { get => masterText; set => masterText = value; }
    public TextMeshProUGUI BgmText { get => bgmText; set => bgmText = value; }
    public TextMeshProUGUI FxText { get => fxText; set => fxText = value; }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}

