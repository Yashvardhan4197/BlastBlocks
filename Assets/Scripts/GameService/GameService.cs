using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    #region SINGLETON_SETUP
    private static GameService instance;
    public static GameService Instance { get { return instance; } }

    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    //DATA
    [SerializeField] BoxView boxPrefab;
    [SerializeField] Transform boxParentTransform;
    [SerializeField] LevelDataSO levelData; //change later to list
    //Services
    private BoxService boxService;
    public BoxService BoxService { get { return boxService; } }
    public LevelDataSO LevelDataSO { get { return levelData; } }

    private void Init()
    {
        boxService =new BoxService(levelData.BoxPositionTransform,boxPrefab,boxParentTransform);
        boxService.OnGameStart();
    }

}
