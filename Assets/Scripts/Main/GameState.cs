using UnityEngine;

public class GameState : MonoBehaviour, IGameStartHandler, IGameRestartHandler, IGamePauseHandler, IGameFinishHandler, IGameDefeatHandler
{
    public static States State { get; private set; }
    public enum States
    {
        Playing = 0,
        Paused = 1,
        Finished = 2,
        Defeated = 3,
        NotStarted
    }

    public void OnGameStarted() => State = States.Playing;
    public void OnGameRestarted() => State = States.Playing;
    public void Defeat() => State = States.Defeated;
    public void OnGameFinished() => State = States.Finished;
    public void OnGamePaused() => State = States.Paused;
    public void OnGameResumed() => State = States.Playing;

    private void Awake() => State = States.NotStarted;

    private void OnEnable()
    {
        GameStarter.Register(this);
        GameRestarter.Register(this);
        GameDefeater.Register(this);
        GameFinisher.Register(this);
        GameSetPause.Register(this);
    }

    private void OnDisable()
    {
        GameStarter.UnRegister(this);
        GameRestarter.UnRegister(this);
        GameDefeater.UnRegister(this);
        GameFinisher.UnRegister(this);
        GameSetPause.UnRegister(this);
    }
}
