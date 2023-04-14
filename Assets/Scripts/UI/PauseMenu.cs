using UnityEngine;

public class PauseMenu : MonoBehaviour, IGamePauseHandler, IGameRestartHandler
{
    [SerializeField] private GameObject _pauseMenu;

    public void OnGamePaused() => _pauseMenu.SetActive(true);
    public void OnGameResumed() => _pauseMenu.SetActive(false);
    public void OnGameRestarted() => _pauseMenu.SetActive(false);

    private void OnEnable()
    {
        GameSetPause.Register(this);
        GameRestarter.Register(this);
    }
    private void OnDisable()
    {
        GameSetPause.UnRegister(this);
        GameRestarter.UnRegister(this);
    }
}
