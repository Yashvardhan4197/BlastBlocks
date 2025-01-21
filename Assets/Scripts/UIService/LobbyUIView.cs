using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIView:MonoBehaviour
{
    private LobbyUIController lobbyUIController;
    [SerializeField] Button startGameButton;
    [SerializeField] Button endGameButton;

    [SerializeField] GameObject LevelSelectionMenu;
    [SerializeField] Button levelSelectionMenuCloseButton;

    public void SetController(LobbyUIController lobbyUIController)
    {
        this.lobbyUIController = lobbyUIController;
    }

    private void Start()
    {
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        endGameButton.onClick.AddListener(OnEndGameButtonClicked);
        levelSelectionMenuCloseButton.onClick.AddListener(CloseLevelSelectionMenu);
    }

    private void CloseLevelSelectionMenu()
    {
        lobbyUIController.CloseLevelSelectionMenu();
    }

    private void OnEndGameButtonClicked()
    {
        lobbyUIController.ExitGame();
    }

    private void OnStartGameButtonClicked()
    {
        //open level selection menu
        lobbyUIController.OpenLevelSelectionMenu();
    }

    public GameObject GetLevelSelectionMenu() => LevelSelectionMenu;

}