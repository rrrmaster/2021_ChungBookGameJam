using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private GameManager gameManager;
    //Todo : �̱��� �и� �ʿ�
    private void Awake()
    {
        gameManager =FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.NextDay();
        }
    }
}
