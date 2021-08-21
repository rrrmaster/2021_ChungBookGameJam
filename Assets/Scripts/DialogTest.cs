using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    public MainPresenter mainPresenter;

    private Dictionary<int, List<TextVO>> dialogTextDictionary = new Dictionary<int, List<TextVO>>();

    private void Start()
    {
        TextAsset dText = Resources.Load("dialogText") as TextAsset;
        GameTextDataVO textData = JsonUtility.FromJson<GameTextDataVO>(dText.ToString());

        foreach (DialogVO vo in textData.list)
        {
            dialogTextDictionary.Add(vo.code, vo.text);
        }

        //mainPresenter.inGameView.StartDialog(dialogTextDictionary[0]);
    }
}
