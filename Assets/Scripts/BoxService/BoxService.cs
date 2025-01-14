
using System.Collections.Generic;
using UnityEngine;

public class BoxService
{
    private BoxPool boxPool;
    private List<Transform> boxPointPositions;
    private Dictionary<Transform, List<BoxController>> instantiatedBoxes;
    public BoxPool BoxPool { get { return boxPool; } }
    public BoxService(BoxView boxPrefab,Transform boxParentTransform)
    {
        //this.boxPointPositions = boxPointPostions;
        boxPool = new BoxPool(boxPrefab,boxParentTransform);
        instantiatedBoxes=new Dictionary<Transform, List<BoxController>>();
    }

    private void InitalizeBoxDict()
    {
        boxPointPositions = GameService.Instance.LevelDataSO[GameService.Instance.LevelService.CurrentLevel].BoxPositionTransform;
        for(int i=0;i<boxPointPositions.Count;i++)
        {
            instantiatedBoxes.Add(boxPointPositions[i],new List<BoxController>());
        }
    }

    public void OnGameStart()
    {
        if (instantiatedBoxes.Count > 0)
        {
            instantiatedBoxes.Clear();
            
        }
        InitalizeBoxDict();
        SpawnBoxes();
    }

    private void SpawnBoxes()
    {
        for (int i=0;i< boxPointPositions.Count; i++)
        {
            //change value later
            for(int j = 0; j < GameService.Instance.LevelDataSO[GameService.Instance.LevelService.CurrentLevel].Columns;j++)
            {
                float currPosZ = boxPointPositions[i].position.z + j*1.1f;
                BoxController newBox = boxPool.GetPooledItem();
                newBox.ActivateBox(new Vector3(boxPointPositions[i].position.x,boxPointPositions[i].position.y,currPosZ));
                instantiatedBoxes[boxPointPositions[i]].Add(newBox);
                newBox.GetBoxMaterial().material.SetColor("_Color",Color.blue);
                if (GameService.Instance.LevelDataSO[GameService.Instance.LevelService.CurrentLevel].Layout[j].ID[i] == 0)
                {
                    newBox.GetBoxMaterial().material.SetColor("_Color", Color.red);
                    newBox.SetColorID(0);
                }else if(GameService.Instance.LevelDataSO[GameService.Instance.LevelService.CurrentLevel].Layout[j].ID[i] == 1)
                {
                    newBox.GetBoxMaterial().material.SetColor("_Color", Color.green);
                    newBox.SetColorID(1);
                }else if(GameService.Instance.LevelDataSO[GameService.Instance.LevelService.CurrentLevel].Layout[j].ID[i] == 2)
                {
                    newBox.GetBoxMaterial().material.SetColor("_Color", Color.blue);
                    newBox.SetColorID(2);
                }
            }
        }
    }

    public void CheckEachFirstBox(int colorID)
    {
        bool checkEmpty = true;
        bool checkColorIDMatch = false;
        foreach(var item in  instantiatedBoxes.Keys)
        {
            if (instantiatedBoxes[item].Count>0&&instantiatedBoxes[item][0].CheckColorID(colorID))
            {
                checkColorIDMatch = true;
                DestroyFirstBlock(item);
            }

            if (instantiatedBoxes[item].Count>0)
            {
                checkEmpty = false;
            }
        }
        if(checkEmpty)
        {
            Debug.Log("GAME WON");
            return;
        }
        if(checkColorIDMatch==false)
        {
            Debug.Log("GAME LOST");
        }
    }

    private void DestroyFirstBlock(Transform item)
    {
        instantiatedBoxes[item][0].ReturnToPool();
        instantiatedBoxes[item].RemoveAt(0);
        MovePreviousBoxes(item);
    }

    private void MovePreviousBoxes(Transform boxSpawnPoint)
    {
        for(int i = 0; i < instantiatedBoxes[boxSpawnPoint].Count;i++)
        {
            instantiatedBoxes[boxSpawnPoint][i].SetPosition(new Vector3(boxSpawnPoint.position.x, boxSpawnPoint.position.y, boxSpawnPoint.position.z+i));
        }
    }
}

