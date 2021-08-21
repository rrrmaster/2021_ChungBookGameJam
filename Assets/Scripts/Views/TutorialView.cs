using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialView : MonoBehaviour
{
    public Transform TutorialChild;

    internal void SetActive(int value)
    {
        for (int i = 0; i < TutorialChild.childCount; i++)
        {
            TutorialChild.GetChild(i).gameObject.SetActive(i== value);
        }
    }
}
