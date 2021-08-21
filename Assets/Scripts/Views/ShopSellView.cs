using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ShopSellView : MonoBehaviour, IPointerUpHandler
{
    [Inject]
    private GameModel gameModel;

    [Inject]
    private ShopModel shopModel;

    public Vector2Int pos;
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(gameModel.Items.ContainsKey(pos))
            {
                var item = gameModel.Items[pos];
                var list = gameModel.StockItems.Where(p => p.SellID == item.ID).ToList();
                if(list.Count > 0)
                {
                    item.Count -= 1;
                    gameModel.Gold.Value += list.First().Price;
                    gameModel.Items[pos] = item;
                    if (item.Count <= 0)
                    {
                        gameModel.Items.Remove(pos);
                    }
                }

                else if(item.ID  < 3)
                {
                    item.Count -= 1;
                    gameModel.Gold.Value += 5;
                    gameModel.Items[pos] = item;
                    if (item.Count <= 0)
                    {
                        gameModel.Items.Remove(pos);
                    }
                }
            }

            Debug.Log("Mouse Click Button : Left"); 
        }
        else if (eventData.button == PointerEventData.InputButton.Right) 
        { 
            Debug.Log("Mouse Click Button : Right"); 
        }

    }


}
