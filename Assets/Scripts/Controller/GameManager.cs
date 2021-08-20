using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Transform box;
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
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = new Vector2Int(Mathf.FloorToInt(mouse.x + 0.5f), Mathf.FloorToInt(mouse.y - 0.5f));
        var vector3 = new Vector3(pos.x + 0.5f, pos.y + 0.5f);
        box.position = vector3;

        if (cropTileMap.HasTile(new Vector3Int(pos.x, pos.y, 0)) && !crops.ContainsKey(pos))
            box.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
        else
            box.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);


        if (Input.GetMouseButtonDown(0) && cropTileMap.HasTile(new Vector3Int(pos.x, pos.y, 0)) )
        {
            if (crops.ContainsKey(pos))
            {
                var crop = crops[pos];
                if(crop.IsRipening.Value)
                {
                    Destroy(crop.gameObject);       
                    crops.Remove(pos);
                }
            }
            else
            {
                var a = Instantiate(crop, vector3 - new Vector3(0, 0.5f), Quaternion.identity);
                crops.Add(pos, a);
            }
        }


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
