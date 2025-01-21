using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] List<LevelDataSO> levelData;
    //Services
    private BoxService boxService;
    private LevelService levelService;
    public BoxService BoxService { get { return boxService; } }
    public LevelService LevelService { get { return levelService; } }
    public List<LevelDataSO> LevelDataSO { get { return levelData; } }
    public UnityAction StartGameAction;
    private void Init()
    {
        levelService= new LevelService();
        boxService =new BoxService(boxPrefab,boxParentTransform);
    }

}
