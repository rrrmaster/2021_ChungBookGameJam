using System;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField]
    private Button inventoryCloseButton;

    [SerializeField]
    public Transform itemTransform;


    public IObservable<Unit> OnInventoryCloseClick
    {
        get => inventoryCloseButton.OnClickAsObservable();
    }
    internal void SetBottom(DictionaryAddEvent<Vector2Int, Item> p)
    {
        Transform transform1 = itemTransform.GetChild(p.Key.x + (2-p.Key.y) * 9).GetChild(0);
        transform1.gameObject.SetActive(true);
        transform1.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<ItemObject>("Items").FirstOrDefault(v => v.ID == p.Value.ID).Icon;
        transform1.GetComponentInChildren<TextMeshProUGUI>().text = p.Value.Count.ToString();
    }
    internal void SetInventoryItemChanged(DictionaryReplaceEvent<Vector2Int, Item> p)
    {
        Transform transform1 = itemTransform.GetChild(p.Key.x + (2 - p.Key.y) * 9).GetChild(0);
        transform1.gameObject.SetActive(true);
        transform1.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<ItemObject>("Items").FirstOrDefault(v => v.ID == p.NewValue.ID).Icon;
        transform1.GetComponentInChildren<TextMeshProUGUI>().text = p.NewValue.Count.ToString();
    }
}