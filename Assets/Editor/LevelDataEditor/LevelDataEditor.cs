
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelDataSO))]
public class LevelDataEditor: Editor
{
    private LevelDataSO levelData;
    private void OnEnable()
    {
        levelData = (LevelDataSO)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();

        DrawGrid();

    }

    private void DrawGrid()
    {
        if (levelData.Layout != null && levelData.Layout.Length > 0)
        {
            for(int i = 0;i<levelData.Layout.Length;i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (levelData.Layout[i] != null && levelData.Layout[i].ID != null)
                {
                    for(int j = 0; j < levelData.Layout[i].ID.Length;j++)
                    {
                        int temp= EditorGUILayout.IntField(levelData.Layout[i].ID[j], GUILayout.Width(40));
                        levelData.SetLayoutValue(i, j, temp);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }

    }
}

