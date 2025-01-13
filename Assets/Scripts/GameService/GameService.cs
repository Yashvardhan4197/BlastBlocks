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
    //[SerializeField] List<LevelDataSO> levelDataCollection;
    [SerializeField] List<Transform> boxPointPositions;
    [SerializeField] BoxView boxPrefab;
    [SerializeField] Transform boxParentTransform;
    //Services
    private BoxService boxService;
    public BoxService BoxService { get { return boxService; } }


    private void Init()
    {
        boxService=new BoxService(boxPointPositions,boxPrefab,boxParentTransform);
    }

}
