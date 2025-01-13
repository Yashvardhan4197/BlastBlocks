
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
        item.enemyController = new BoxController(boxPrefab,boxParentTransform);
        item.isUsed = true;
        pooledItems.Add(item);
        return item.enemyController;
    }

    public void OnGameStart()
    {
        foreach (var item in pooledItems)
        {
            item.enemyController.ReturnToPool();
        }
    }

    public BoxController GetPooledItem()
    {
        PooledItem item = pooledItems.Find(item => item.isUsed == false);
        if (item != null)
        {
            item.isUsed = true;
            return item.enemyController;
        }
        return CreatePooledItem();
    }

    public void ReturnToPool(BoxController enemyController)
    {
        PooledItem item = pooledItems.Find(item => item.enemyController == enemyController);
        if (item != null)
        {
            item.isUsed = false;
        }
    }

    public class PooledItem
    {
        public BoxController enemyController;
        public bool isUsed;
    }
}
