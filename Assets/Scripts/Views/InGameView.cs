using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameView : MonoBehaviour
{
    [SerializeField] private DialogPanel dialogPanel;

    public void StartDialog(List<TextVO> list)
    {
        dialogPanel.StartDialog(list);
    }
}
