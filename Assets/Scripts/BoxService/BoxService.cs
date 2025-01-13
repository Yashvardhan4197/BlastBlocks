using System;
using System.Collections.Generic;
using UnityEngine;

public class BoxService
{
    private BoxPool boxPool;
    private List<Transform> boxPointPositions;
    private Dictionary<Transform, List<BoxView>> instantiatedBoxes;
    public BoxPool BoxPool { get { return boxPool; } }
    public BoxService(List<Transform> boxPointPostions,BoxView boxPrefab,Transform boxParentTransform)
    {
        this.boxPointPositions = boxPointPostions;
        boxPool = new BoxPool(boxPrefab,boxParentTransform);
    }

    public void OnGameStart()
    {
        SpawnBoxes();
    }

    private void SpawnBoxes()
    {
        for(int i=0;i<boxPointPositions.Count; i++)
        {
            for(int j=0;j<10;j++)
            {
                float currPosY = boxPointPositions[i].position.y - i;
                BoxController newBox = boxPool.GetPooledItem();
                newBox.ActivateBox(new Vector3(boxPointPositions[j].position.x, currPosY, boxPointPositions[j].position.z));
                //add to the dictionary
            }
        }
    }
}

