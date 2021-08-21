using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarView : MonoBehaviour
{
    public Transform dayItem;
    public TextMeshProUGUI dayText;
    public Image calendarImage;
    public Sprite[] calendarSeason;
    public Button closeButton;

    internal void SetCalendar(DateTime p)
    {
        var dateTime = new DateTime(p.Year, p.Month,1);
        var days = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        var weekStart = (int)dateTime.DayOfWeek;

        for (int i = 0; i < dayItem.childCount; i++)
        {
            dayItem.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }

        for (int i = 0; i < days ; i++)
        {
            dayItem.GetChild(i + weekStart).GetChild(0).gameObject.SetActive(true);
            dayItem.GetChild(i + weekStart).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
        }

    }
}
