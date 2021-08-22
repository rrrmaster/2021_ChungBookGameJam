using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDungeonQuitPanel : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private DungeonManager dungeonManager;

    private void Start()
    {
        dungeonManager = FindObjectOfType<DungeonManager>();

        yesButton.onClick.AddListener(() =>
        {
            dungeonManager.QuitDungeon();
            gameObject.SetActive(false);
        });
        noButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
