using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class InventoryPresenter : IInitializable, IDisposable
{
    private readonly InventoryView inventoryView;

    private readonly CompositeDisposable compositeDisposable;
    private readonly GameModel gameModel;
    public InventoryPresenter(InventoryView inventoryView,GameModel gameModel)
    {
        this.gameModel = gameModel;
        this.inventoryView = inventoryView;
        compositeDisposable = new CompositeDisposable();
    }

    public void Initialize()
    {
        gameModel.Items.ObserveAdd().Subscribe(v => inventoryView.SetBottom(v));
        gameModel.Items.ObserveReplace().Subscribe(v => inventoryView.SetInventoryItemChanged(v));
        inventoryView.OnInventoryCloseClick.Subscribe(_=>inventoryView.gameObject.SetActive(false));
    }


    public void Dispose()
    {
        compositeDisposable.Dispose();
    }
}