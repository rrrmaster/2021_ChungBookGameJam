using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Image dimmad;
    public Button StartButton
    {
        get => startButton;
    }

    public Button OptionButton
    {
        get => optionButton;
    }

    public Button ExitButton
    {
        get => exitButton;
    }

    public void Dimmad(Action res)
    {
        dimmad.DOFade(1, 1).From(0).OnStart(() => dimmad.gameObject.SetActive(true)).OnComplete(()=>res());
    }
}
