using UnityEngine;


[CreateAssetMenu(fileName ="NewLevelData",menuName ="ScriptableObjects/LevelData")]
public class LevelDataSO: ScriptableObject
{
    [SerializeField] int rows;
    public int Rows {  get { return rows; } }
}