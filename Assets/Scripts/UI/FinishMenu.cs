using UnityEngine;

public class FinishMenu : MonoBehaviour, IGameFinishHandler, IGameRestartHandler
{
    [SerializeField] private GameObject _menu;

    public void OnGameFinished() => _menu.SetActive(true);
    public void OnGameRestarted() => _menu.SetActive(false);

    private void OnEnable()
    {
        GameFinisher.Register(this);
        GameRestarter.Register(this);
    }
    private void OnDisable()
    {
        GameFinisher.UnRegister(this);
        GameRestarter.UnRegister(this);
    }
}
