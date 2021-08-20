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
        set => dateText.text = value.ToString("yyy년 MM월 dd일");
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
                { Season.Spring, "봄"},
                { Season.Summer, "여름"},
                { Season.Autumn, "가을"},
                { Season.Winter, "겨울"},
            }[value];
            seasonText.text = $"계절 {v}";
        }
    }
}
