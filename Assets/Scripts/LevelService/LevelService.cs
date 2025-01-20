
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelService
{
    private int currentLevel=0;

    public int CurrentLevel {  get { return currentLevel; } }

    public void LoadScene(int level)
    {
        if (level > SceneManager.sceneCount)
        {
            currentLevel = 1;
        }
        else { currentLevel = level; }
        SceneManager.LoadScene(currentLevel);
        Time.timeScale = 1f;
    }

    public void SetCurrentLevelStatusCompleted()
    {
        PlayerPrefs.SetInt("Level" + currentLevel, 1);
        if(currentLevel+1< SceneManager.sceneCount)
        {
            if(PlayerPrefs.GetInt("Level"+(currentLevel+1),-1)==-1)
            {
                PlayerPrefs.SetInt("Level" + (currentLevel + 1), 0);
            }
        }
    }

}
