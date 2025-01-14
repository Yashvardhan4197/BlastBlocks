
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
    }

}
