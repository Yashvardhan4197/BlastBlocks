
using UnityEngine;
using UnityEngine.UI;

public class InGameUIService : MonoBehaviour
{
    private InGameUIController inGameUIController;
    [SerializeField] InGameUIView inGameUIView;
    private bool isPaused;
    [SerializeField] Button PauseButton;
    private void Awake()
    {
        inGameUIController = new InGameUIController(inGameUIView);
    }

    private void Start()
    {
        CloseGamePausedSection();
        CloseGameWonLostPopUp();
        PauseButton.onClick.AddListener(TogglePause);
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            CloseGamePausedSection();
        }
        else
        {
            OpenGamePausedSection();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }


    public void OpenGamePausedSection()
    {
        inGameUIController.OpenPauseSection();
        isPaused = true;
    }

    public void CloseGamePausedSection()
    {
        inGameUIController.ClosePauseSection();
        isPaused = false;
    }

    public void OnGameWon()
    {
        inGameUIController.SetGameWon();
        inGameUIController.OpenGameWonLostPopUp();
    }

    public void OnGameLost()
    {
        inGameUIController.SetGameLost();
        inGameUIController.OpenGameWonLostPopUp();
    }

    public void CloseGameWonLostPopUp()
    {
        inGameUIController.CloseGameWonLostPopUp();
    }

}
