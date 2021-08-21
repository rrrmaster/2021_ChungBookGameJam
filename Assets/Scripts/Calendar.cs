using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Calendar : MonoBehaviour
{
    [Inject]
    private CalendarPresenter calendarPresenter;
    private bool isEnter;

    private void Update()
    {
        if (isEnter && Input.GetKeyDown(KeyCode.Space))
            calendarPresenter.ShowCalendar();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = false;
        }
    }
}
