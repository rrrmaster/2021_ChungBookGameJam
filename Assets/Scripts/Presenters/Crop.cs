using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;
using Zenject;

public class Crop : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public int id;
    public Sprite[] cropSprites;
    public int maxGrow;
    public ReactiveProperty<DateTime> LastWater;
    public ReactiveProperty<int> Grow;
    public bool IsTodayWashing;
    public ReadOnlyReactiveProperty<bool> IsRipening;

    [Inject]
    private GameModel gameModel;
    private void Awake()
    {
        LastWater = new ReactiveProperty<DateTime>(DateTime.MinValue);
        Grow.Subscribe(value =>
        {
            Debug.Log(value);
            int v1 = (cropSprites.Length - 1);
            int v = Mathf.Min(v1, (int)(v1 * ((float)value / maxGrow)));
            spriteRenderer.sprite = cropSprites[v];
        });
        IsTodayWashing = false;
        IsRipening = Grow.Select(x => x >= maxGrow).ToReadOnlyReactiveProperty();
    }


    internal void SetData(int id)
    {
        this.id = id;
        var cropObject = Resources.LoadAll<CropObject>("Crops").FirstOrDefault(p => p.ID == id);
        cropSprites = cropObject.Animation;
        Grow.SetValueAndForceNotify(0);
    }
}
