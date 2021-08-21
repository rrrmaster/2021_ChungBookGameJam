using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

public class Crop : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public int id;
    public Sprite[] cropSprites;
    public int maxGrow;
    public ReactiveProperty<int> Grow;
    public ReadOnlyReactiveProperty<bool> IsRipening;

    private void Awake()
    {
        Grow.Subscribe(value =>
        {
            Debug.Log(value);
            int v1 = (cropSprites.Length - 1);
            int v = Mathf.Min(v1, (int)(v1 * ((float)value / maxGrow)));
            spriteRenderer.sprite = cropSprites[v];
        });
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
