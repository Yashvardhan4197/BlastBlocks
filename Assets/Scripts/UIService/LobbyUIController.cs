using System;
using UnityEngine;

public class LobbyUIController
{
    private LobbyUIView lobbyUIView;
    public LobbyUIController(LobbyUIView lobbyUIView)
    {
        this.lobbyUIView = lobbyUIView;
        this.lobbyUIView.SetController(this);
    }

    public void OpenLevelSelectionMenu()
    {
        lobbyUIView.GetLevelSelectionMenu().SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseLevelSelectionMenu()
    {
        lobbyUIView.GetLevelSelectionMenu().SetActive(false);
    }
}