using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonQuitDoor : MonoBehaviour
{
    public UIDungeonQuitPanel dungeonQuitPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dungeonQuitPanel.gameObject.SetActive(true);
            //TODO 팝업 Quit 패널
        }
    }
}
