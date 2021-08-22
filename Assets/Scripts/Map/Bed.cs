using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bed : MonoBehaviour
{
    [Inject]
    private GamePresenter gamePresenter;
    private bool isEnter;

    private void Update()
    {
        if(isEnter && Input.GetKeyDown(KeyCode.Space))
        {

            SoundManager.Instance.PlayFXSound("Bed");
            gamePresenter.NextDay();
        }
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
