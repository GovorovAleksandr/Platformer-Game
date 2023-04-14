using UnityEngine;
using System.Collections;

public class BaseEffect : MonoBehaviour, IGameStartHandler, IGameRestartHandler, IGameDefeatHandler
{
    [SerializeField] private float StartOffset;
    [SerializeField] private float Duration;
    
    protected bool Started;

    public void OnGameStarted() => Restart();
    public void OnGameRestarted() => Restart();
    public void Defeat() => StartCoroutine(ToggleIsStarted(Duration, false));

    private void Restart()
    {
        StopAllCoroutines();
        StartCoroutine(ToggleIsStarted(StartOffset, true));
    }

    private IEnumerator ToggleIsStarted(float time, bool isStarted)
    {
        var timer = new Timer();
        StartCoroutine(timer.Restart(time));

        yield return new WaitUntil(() => timer.IsOver);

        Started = isStarted;
    }

    private void OnEnable()
    {
        GameStarter.Register(this);
        GameRestarter.Register(this);
        GameDefeater.Register(this);
    }

    private void OnDisable()
    {
        GameStarter.UnRegister(this);
        GameRestarter.UnRegister(this);
        GameDefeater.UnRegister(this);
    }
}
