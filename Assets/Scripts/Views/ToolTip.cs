using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToolTip : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector2 pos)
    {
        var rect = GetComponent<RectTransform>();
        rect.position = pos;
    }

    public void SetText(string name,string description)
    {
        nameText.text = name;
        descriptionText.text = description;

    }

}
