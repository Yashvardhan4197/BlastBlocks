
using System.Collections.Generic;
using UnityEngine;

public class BoxPool
{
    private BoxView boxPrefab;
    private Transform boxParentTransform;
    private List<PooledItem> pooledItems = new List<PooledItem>();
    public BoxPool(BoxView boxPrefab,Transform boxParentTransform)
    {
        this.boxPrefab = boxPrefab;
        this.boxParentTransform = boxParentTransform;
        //GameService.Instance.startGameAction += OnGameStart;
    }
    private BoxController CreatePooledItem()
    {
        PooledItem item = new PooledItem();
        item.boxController = new BoxController(boxPrefab,boxParentTransform);
        item.isUsed = true;
        pooledItems.Add(item);
        return item.boxController;
    }

    public void OnGameStart()
    {
        foreach (var item in pooledItems)
        {
            item.boxController.ReturnToPool();
        }
    }

    public BoxController GetPooledItem()
    {
        PooledItem item = pooledItems.Find(item => item.isUsed == false);
        if (item != null)
        {
            item.isUsed = true;
            return item.boxController;
        }
        return CreatePooledItem();
    }

    public void ReturnToPool(BoxController boxController)
    {
        PooledItem item = pooledItems.Find(item => item.boxController == boxController);
        if (item != null)
        {
            item.isUsed = false;
        }
    }

    public class PooledItem
    {
        public BoxController boxController;
        public bool isUsed;
    }
}
