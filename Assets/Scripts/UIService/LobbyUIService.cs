
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIService: MonoBehaviour
{
    [SerializeField] LobbyUIView lobbyUIView;
    private LobbyUIController lobbyUIController;
    [SerializeField] List<LevelButtonView> levelSelectionButtons;
    private void Awake()
    {
        lobbyUIController = new LobbyUIController(lobbyUIView);
    }

    private void Start()
    {
        SetLevelButtonsStatus();
        lobbyUIController.CloseLevelSelectionMenu();
    }

    private void SetLevelButtonsStatus()
    {
        for(int i=0;i<levelSelectionButtons.Count; i++)
        {
            int status = PlayerPrefs.GetInt("Level" + (i + 1), -1);
            Debug.Log("Level" + (i + 1) + " " + status);
            switch (status)
            {
                case -1:
                    if (i == 0) 
                    {
                        levelSelectionButtons[i].GetLevelButton().enabled = true;
                        levelSelectionButtons[i].GetLevelButton().image.color = Color.white;
                    }
                    else
                    {
                        levelSelectionButtons[i].GetLevelButton().enabled = false;
                        levelSelectionButtons[i].GetLevelButton().image.color = Color.grey;
                    }
                    break;
                case 0:
                    levelSelectionButtons[i].GetLevelButton().enabled = true;
                    levelSelectionButtons[i].GetLevelButton().image.color = Color.white;
                    break;
                case 1:
                    levelSelectionButtons[i].GetLevelButton().enabled = true;
                    levelSelectionButtons[i].GetLevelButton().image.color = Color.green;
                    if(PlayerPrefs.GetInt("Level" + (i + 2), -1)==-1)
                    {
                        PlayerPrefs.SetInt("Level" + (i + 2), 0);
                    }
                    break;
            }
            levelSelectionButtons[i].GetLevelText().text = (i + 1).ToString();
        }
    }

    public void OnLevelButtonClicked(int levelNumber)
    {
        GameService.Instance.LevelService.LoadScene(levelNumber);
    }


}
