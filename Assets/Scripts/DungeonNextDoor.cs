using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonNextDoor : MonoBehaviour
{
    private DungeonManager dungeonManager;

    private void Start()
    {
        dungeonManager = GetComponentInParent<DungeonManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dungeonManager.OnClickDungeonButton();
            //TODO NEXT로
        }
    }
}
