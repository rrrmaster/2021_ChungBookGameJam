using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Dictionary<Vector2Int, Crop> crops;
    public Crop crop;
    public GamePresenter gamePresenter;
    public Tilemap cropTileMap;
    private void Awake()
    {
        crops = new Dictionary<Vector2Int, Crop>();
    }

    private void Update()
    {
   


    }
    public void NextDay()
    {
        foreach (var item in crops)
        {
            item.Value.Grow.Value += 1;
        }
        gamePresenter.NextDay();
    }
}
