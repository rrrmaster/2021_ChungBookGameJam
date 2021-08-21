using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryPresenter : IInitializable, IDisposable
{
    private readonly InventoryView inventoryView;

    private readonly CompositeDisposable compositeDisposable;
    private readonly GameModel gameModel;
    public InventoryPresenter(InventoryView inventoryView, GameModel gameModel)
    {
        this.gameModel = gameModel;
        this.inventoryView = inventoryView;
        compositeDisposable = new CompositeDisposable();
    }

    public void Initialize()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                var x_ = x;
                var y_ = y;
                InventoryItemView inventoryItemView = inventoryView.itemTransform.GetChild((2 - y) * 9 + x).GetComponent<InventoryItemView>();
                inventoryItemView.gameModel = gameModel;
                inventoryItemView.pos = new Vector2Int(x_, y_);
                inventoryView.itemTransform.GetChild((2 - y) * 9 + x).GetComponent<Button>().OnClickAsObservable().Subscribe(_ => Swap(x_,y_));

            }
        }

        gameModel.Items.ObserveAdd().Subscribe(v => inventoryView.SetBottom(v));
        gameModel.Items.ObserveRemove().Subscribe(p => inventoryView.SetInventoryItemRemoved(p));
        gameModel.Items.ObserveReplace().Subscribe(v => inventoryView.SetInventoryItemChanged(v));
        inventoryView.OnInventoryCloseClick.Subscribe(_ => inventoryView.gameObject.SetActive(false));
    }
    private Vector2Int index_0 = new Vector2Int(-1, -1);
    private Vector2Int index_1 = new Vector2Int(-1, -1);
    private void Swap(int x, int y)
    {
        if (index_0 == new Vector2Int(-1, -1))
        {
            index_0 = new Vector2Int(x, y);
            inventoryView.itemTransform.GetChild((2 - y) * 9 + x).GetComponent<Button>().targetGraphic.color = new Color32(51, 187, 149, 255);
        }
        else if (index_1 == new Vector2Int(-1, -1))
        {
            index_1 = new Vector2Int(x, y);
            inventoryView.itemTransform.GetChild((2 - y) * 9 + x).GetComponent<Button>().targetGraphic.color = new Color32(51,187,149,255);
            if (gameModel.Items.ContainsKey(index_0) && gameModel.Items.ContainsKey(index_1))
            {
                var temp = gameModel.Items[index_0];
                gameModel.Items[index_0] = gameModel.Items[index_1];
                gameModel.Items[index_1] = temp;
            }
            else if (gameModel.Items.ContainsKey(index_0) && !gameModel.Items.ContainsKey(index_1))
            {
                Item temp = gameModel.Items[index_0];
                gameModel.Items[index_1] = temp;
                gameModel.Items.Remove(index_0);
            }
            else if (!gameModel.Items.ContainsKey(index_0) && gameModel.Items.ContainsKey(index_1))
            {
                Item temp = gameModel.Items[index_1];
                gameModel.Items[index_0] = temp;
                gameModel.Items.Remove(index_1);

            }
            inventoryView.itemTransform.GetChild((2 - index_0.y) * 9 + index_0.x).GetComponent<Button>().targetGraphic.color = Color.white;
            inventoryView.itemTransform.GetChild((2 - index_1.y) * 9 + index_1.x).GetComponent<Button>().targetGraphic.color = Color.white;
            index_0 = new Vector2Int(-1, -1);   
            index_1 = new Vector2Int(-1, -1);
        }
    }

    public void Dispose()
    {
        compositeDisposable.Dispose();
    }
}