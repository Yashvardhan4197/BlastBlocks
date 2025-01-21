using System;
using UnityEngine;

public class InGameUIController
{
    private InGameUIView inGameUIView;

    public InGameUIController(InGameUIView inGameUIView)
    {
        this.inGameUIView = inGameUIView;
        this.inGameUIView.SetController(this);
    }

    public void CloseGameWonLostPopUp()
    {
        inGameUIView.GetGameWonLostPopUp().SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClosePauseSection()
    {
        inGameUIView.GetGamePausedSection().SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenGameWonLostPopUp()
    {
        inGameUIView.GetGameWonLostPopUp().SetActive(true);
        Time.timeScale = 0f;
    }

    public void OpenPauseSection()
    {
        inGameUIView.GetGamePausedSection().SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetGameWon()
    {
        inGameUIView.GetGameWonLostText().text = "GAME WON";
        inGameUIView.GetNextLevelButton().gameObject.SetActive(true);
    }

    public void SetGameLost()
    {
        inGameUIView.GetGameWonLostText().text = "GAME LOST";
        inGameUIView.GetNextLevelButton().gameObject.SetActive(false);
    }

    public void OpenNextLevel()
    {
        GameService.Instance.SoundService.PlaySFX(Sound.BUTTON_CLICK);
        GameService.Instance.LevelService.LoadScene(GameService.Instance.LevelService.CurrentLevel + 1);
    }

    public void OpenLobby()
    {
        GameService.Instance.SoundService.PlaySFX(Sound.BUTTON_CLICK);
        GameService.Instance.LevelService.LoadScene(0);
    }

    public void RestartLevel()
    {
        GameService.Instance.SoundService.PlaySFX(Sound.BUTTON_CLICK);
        GameService.Instance.LevelService.LoadScene(GameService.Instance.LevelService.CurrentLevel);
    }
}