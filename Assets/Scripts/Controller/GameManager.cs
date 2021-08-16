using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform box;
    public Dictionary<Vector2Int, Crop> crops;
    public Crop crop;
    public GamePresenter gamePresenter;
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
        if (Input.GetMouseButtonDown(0) && !crops.ContainsKey(pos))
        {
            var a = Instantiate(crop, vector3 - new Vector3(0, 0.5f), Quaternion.identity);
            crops.Add(pos, a);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var item in crops)
            {
                item.Value.grow.Value += 1;
            }
            gamePresenter.NextDay();
        }
    }

}
