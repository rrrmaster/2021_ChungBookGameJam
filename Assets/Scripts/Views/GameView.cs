using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using DG.Tweening;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dateText;

    [SerializeField]
    private TextMeshProUGUI goldText;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI seasonText;

    [SerializeField]
    private Image seasonPanel;

    [SerializeField]
    private Sprite[] seasonPanelImages;

    [SerializeField]
    private Button shopOnButton;

    [SerializeField]
    private Transform inventoryList;

    [SerializeField]
    private Image clockImage;

    [SerializeField]
    private Sprite[] clocks; 

    [SerializeField]
    private Transform box;
    [SerializeField]
    private TextMeshProUGUI modeText;

    [SerializeField]
    private Image dimmed;

    public IObservable<Unit> OnShopClick
    {
        get => shopOnButton.OnClickAsObservable();
    }
    public DateTime DateText
    {
        
        set => dateText.text = value.ToString("yyy년 MM월 dd일");
    }
    public DateTime TimeText
    {
        set => timeText.text = $"{value:tt hh:mm}";
    }
    public DateTime ClockImage {
        set => clockImage.sprite =  (5 <= value.Hour  && value.Hour <= 17) ? clocks[0] : clocks[1];
    }

    public int GoldText
    {
        set => goldText.text = $"{value:N0}G";
    }
    public Transform Box
    {
        get => box;
    }

 

    public Season SeasonText
    {
        set
        {
            string v = new Dictionary<Season, string>() {
                { Season.Spring, "봄"},
                { Season.Summer, "여름"},
                { Season.Autumn, "가을"},
                { Season.Winter, "겨울"},
            }[value];
            seasonText.text = $"{v}";
        }
    }
    public Season SeasonPanel
    {
        set 
        {
            var v = new Dictionary<Season, int>() {
                { Season.Spring, 0},
                { Season.Summer, 1},
                { Season.Autumn, 2},
                { Season.Winter, 3},
            }[value];
            seasonPanel.sprite = seasonPanelImages[v];
        }
    }

    internal void UseItemMode(bool value)
    {
        box.gameObject.SetActive(value);
        modeText.gameObject.SetActive(value);
    }
    internal void SetBottom(DictionaryAddEvent<Vector2Int, Item> p)
    {
        Transform transform1 = inventoryList.GetChild(p.Key.x).GetChild(0);
        transform1.gameObject.SetActive(true);
        transform1.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<ItemObject>("Items").FirstOrDefault(v => v.ID == p.Value.ID).Icon;
        transform1.GetComponentInChildren<TextMeshProUGUI>().text = $"{p.Value.Count:N0}";
    }

    internal void SetBottom(DictionaryRemoveEvent<Vector2Int, Item> p)
    {
        Transform transform1 = inventoryList.GetChild(p.Key.x).GetChild(0);
        transform1.gameObject.SetActive(false);
    }

    internal void SetBottomChanged(DictionaryReplaceEvent<Vector2Int, Item> p)
    {
        Transform transform1 = inventoryList.GetChild(p.Key.x).GetChild(0);
        transform1.gameObject.SetActive(true);
        transform1.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<ItemObject>("Items").FirstOrDefault(v => v.ID == p.NewValue.ID).Icon;
        transform1.GetComponentInChildren<TextMeshProUGUI>().text = $"{p.NewValue.Count:N0}";
    }

    internal void NextDay(Action p)
    {
        var seq = DOTween.Sequence();

        seq.OnStart(() => {dimmed.gameObject.SetActive(true);});
        seq.OnComplete(() => { dimmed.gameObject.SetActive(false); p?.Invoke(); }).Play();
        seq.Append(dimmed.DOFade(1, 0.4f).From(0));
        seq.Append(dimmed.DOFade(0, 0.4f).From(1).SetDelay(1));
        seq.Play();
    }
}
