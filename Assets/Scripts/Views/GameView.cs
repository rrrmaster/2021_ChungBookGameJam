using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dateText;

    [SerializeField]
    private TextMeshProUGUI goldText;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI seasonText;

    public DateTime DateText
    {
        set => dateText.text = value.ToString("yyy�� MM�� dd��");
    }
    public DateTime TimeText
    {
        set => timeText.text = $"{(value.Hour < 12 ? "AM" : "PM") } {(value.Hour + 1) % 12 + 1}:{value.Minute}";
    }

    public int GoldText
    {
        set => goldText.text = $"{value:N0}G";
    }


    public Season SeasonText
    {
        set
        {
            string v = new Dictionary<Season, string>() {
                { Season.Spring, "��"},
                { Season.Summer, "����"},
                { Season.Autumn, "����"},
                { Season.Winter, "�ܿ�"},
            }[value];
            seasonText.text = $"���� {v}";
        }
    }
}
