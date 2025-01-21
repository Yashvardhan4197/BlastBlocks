using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIView : MonoBehaviour
{
    private InGameUIController inGameUIController;
    [SerializeField] GameObject gameWonLostPopUp;
    [SerializeField] GameObject gamePausedSection;
    [SerializeField] TextMeshProUGUI gameWonLostText;

    [SerializeField] Button nextlevelButton;
    [SerializeField] Button restartButtonGameWonLostPopUpButton;
    [SerializeField] Button exitButtonGameWonLostPopUpButton;

    [SerializeField] Button resumeGameButton;
    [SerializeField] Button restartGameButtonPauseMenu;
    [SerializeField] Button exitGameButtonPauseMenu;
    private void Start()
    {
        nextlevelButton.onClick.AddListener(SelectNextLevel);
        restartButtonGameWonLostPopUpButton.onClick.AddListener(RestartLevel);
        exitButtonGameWonLostPopUpButton.onClick.AddListener(ExitGame);
        restartGameButtonPauseMenu.onClick.AddListener(RestartLevel);
        resumeGameButton.onClick.AddListener(ResumeGameFromPaused);
        exitGameButtonPauseMenu.onClick.AddListener(ExitGame);
    }

    private void ResumeGameFromPaused()
    {
        inGameUIController.ClosePauseSection();
    }

    private void ExitGame()
    {
        inGameUIController.OpenLobby();
    }

    private void RestartLevel()
    {
        inGameUIController.RestartLevel();
    }

    private void SelectNextLevel()
    {
        inGameUIController.OpenNextLevel();
    }

    public void SetController(InGameUIController inGameUIController)
    {
        this.inGameUIController = inGameUIController;
    }
    public GameObject GetGameWonLostPopUp() => gameWonLostPopUp;
    public GameObject GetGamePausedSection() => gamePausedSection;
    public TextMeshProUGUI GetGameWonLostText() => gameWonLostText;
    public Button GetNextLevelButton() => nextlevelButton;
}
