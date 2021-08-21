using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameQuitPanel : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Start()
    {
        yesButton.onClick.AddListener(() => Application.Quit());
        noButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
