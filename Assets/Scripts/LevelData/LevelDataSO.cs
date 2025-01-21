using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewLevelData",menuName ="ScriptableObjects/LevelData")]
public class LevelDataSO: ScriptableObject
{
    [SerializeField] int columns;
    [SerializeField] List<Transform> boxPositionTransform=new List<Transform>();
    [SerializeField] BoxDataContainer[] layout;
    private int previousColumns=-1;

    public int Columns {  get { return columns; } }
    public List<Transform> BoxPositionTransform { get {  return boxPositionTransform; } }
    public BoxDataContainer[] Layout { get { return layout; } }


    public void SetLayoutValue(int i,int j,int val)
    {
        layout[i].ID[j] = val;
    }

    [Serializable]
    public class BoxDataContainer
    {
        public int[] ID;

        public BoxDataContainer(int column)
        {
            ID = new int[column];
        }
    }

    private void OnValidate()
    {
        if (columns != previousColumns)
        {
            previousColumns = columns;
            BoxDataContainer[] newLayout = new BoxDataContainer[columns];

            for (int i = 0; i < columns; i++)
            {
                if (i < layout?.Length)
                {
                    newLayout[i] = layout[i];
                }
                else
                {
                    newLayout[i] = new BoxDataContainer(boxPositionTransform.Count);
                }
            }
            layout = newLayout;
            foreach (var box in layout)
            {
                if (box.ID.Length != boxPositionTransform.Count)
                {
                    Array.Resize(ref box.ID, boxPositionTransform.Count);
                }
            }
        }
    }

    private void Awake()
    {
        if(layout==null)
        {
            ResetBoxDataContainer();
        }
    }

    private void ResetBoxDataContainer()
    {
        layout = new BoxDataContainer[columns];
        for(int i=0;i<columns;i++)
        {
            BoxDataContainer box = new BoxDataContainer(boxPositionTransform.Count);
            layout[i] = box;
            for(int j=0;j<boxPositionTransform.Count;j++)
            {
                box.ID[j] = 0;
            }
        }
    }
}

public enum BoxColorID
{
    RED,
    GREEN, 
    BLUE
}