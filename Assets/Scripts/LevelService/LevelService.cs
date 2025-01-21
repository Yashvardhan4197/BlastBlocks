
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelService
{
    private int currentLevel=0;

    public int CurrentLevel {  get { return currentLevel; } }

    public void LoadScene(int level)
    {
        if (level > SceneManager.sceneCountInBuildSettings+1)
        {
            Debug.Log("CurrentLevel: " + level);
            Debug.Log("totalScenes: "+ SceneManager.sceneCountInBuildSettings);
            Debug.Log("maxScenes: " + SceneManager.sceneCountInBuildSettings);
            currentLevel = 1;
        }
        else { currentLevel = level; }
        SceneManager.LoadScene(currentLevel);
        if (currentLevel != 0)
        {
            GameService.Instance.StartGameAction?.Invoke();
        }
        Time.timeScale = 1f;
    }

    public void SetCurrentLevelStatusCompleted()
    {
        PlayerPrefs.SetInt("Level" + currentLevel, 1);
        if(currentLevel+1< SceneManager.sceneCountInBuildSettings+1)
        {
            if(PlayerPrefs.GetInt("Level"+(currentLevel+1),-1)==-1)
            {
                PlayerPrefs.SetInt("Level" + (currentLevel + 1), 0);
            }
        }
    }

}
