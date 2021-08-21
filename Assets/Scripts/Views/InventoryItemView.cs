using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemView : MonoBehaviour, IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2Int pos;
    public GameModel gameModel;
    private ItemObject itemObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameModel.Items.ContainsKey(pos))
        {
            itemObject = gameModel.ItemObjects.First(p => p.ID == gameModel.Items[pos].ID);
            FindObjectOfType<ToolTip>(true).Show();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameModel.Items.ContainsKey(pos))
        {
            FindObjectOfType<ToolTip>(true).Hide();
            itemObject = null;
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(gameModel.Items.ContainsKey(pos) && itemObject)
        {

        ToolTip toolTip1 = FindObjectOfType<ToolTip>(true);
        toolTip1.SetPosition(eventData.position);
        toolTip1.SetText(itemObject.Name, itemObject.Description);
        }
    }

}
