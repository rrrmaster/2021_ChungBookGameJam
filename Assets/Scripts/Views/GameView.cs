using TMPro;
using System;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dateText;

    [SerializeField]
    private TextMeshProUGUI goldText;

    public DateTime DateText
    {
        set => dateText.text = value.ToString("yyy�� MM�� dd��");
    }

    public int GoldText
    {
        set => goldText.text = $"��� {value:N}";
    }
}
