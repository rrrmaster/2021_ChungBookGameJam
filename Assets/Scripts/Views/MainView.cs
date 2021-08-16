using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button exitButton;

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
}
