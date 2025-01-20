
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIService: MonoBehaviour
{
    [SerializeField] LobbyUIView lobbyUIView;
    private LobbyUIController lobbyUIController;
    [SerializeField] List<Button> levelSelectionButtons;
    private void Awake()
    {
        lobbyUIController = new LobbyUIController(lobbyUIView);
    }

    private void Start()
    {
        SetLevelButtonsStatus();
    }

    private void SetLevelButtonsStatus()
    {
        for(int i=0;i<levelSelectionButtons.Count; i++)
        {
            int status = PlayerPrefs.GetInt("Level" + (i + 1), -1);
            switch (status)
            {
                case -1:
                    if (i == 0) 
                    {
                        levelSelectionButtons[i].enabled = true;
                        levelSelectionButtons[i].image.color = Color.white;
                    }
                    else
                    {
                        levelSelectionButtons[i].enabled = false;
                        levelSelectionButtons[i].image.color = Color.grey;
                    }
                    break;
                case 0:
                    levelSelectionButtons[i].enabled = true;
                    levelSelectionButtons[i].image.color = Color.white;
                    break;
                case 1:
                    levelSelectionButtons[i].enabled = true;
                    levelSelectionButtons[i].image.color = Color.green;
                    break;
            }

        }
    }

    public void OnLevelButtonClicked(int levelNumber)
    {
        GameService.Instance.LevelService.LoadScene(levelNumber);
    }


}
