using UnityEngine;

public class RestartMenu : MonoBehaviour, IGameDefeatHandler, IGameRestartHandler
{
    [SerializeField] private GameObject _restartMenu;

    public void Defeat() => _restartMenu.SetActive(true);
    public void OnGameRestarted() => _restartMenu.SetActive(false);

    private void OnEnable()
    {
        GameDefeater.Register(this);
        GameRestarter.Register(this);
    }
    private void OnDisable()
    {
        GameDefeater.UnRegister(this);
        GameRestarter.UnRegister(this);
    }
}
