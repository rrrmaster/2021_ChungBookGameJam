using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Crop : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public Sprite[] cropSprites;
    public int maxGrow;
    public ReactiveProperty<int> grow;

    private void Awake()
    {
        grow.Subscribe(value =>
        {
            Debug.Log(value);
            int v1 = (cropSprites.Length - 1);
            int v = Mathf.Min(v1, (int)(v1 * ((float)value / maxGrow)));
            spriteRenderer.sprite = cropSprites[v];
        });
    }
}
