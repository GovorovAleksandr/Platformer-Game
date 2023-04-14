using UnityEngine;
using System.Collections.Generic;

public class GameSetPause : MonoBehaviour, IGameRestartHandler
{
    private static readonly List<IGamePauseHandler> _handlers = new();
    private UserInput _input;

    public void OnGameRestarted() => Resume();

    public static void Pause()
    {
        if (!TabsList.AllClosed) return;

        foreach (var handler in _handlers.ToArray()) handler.OnGamePaused();
    }

    public static void Resume()
    {
        if (!TabsList.AllClosed) return;

        foreach (var handler in _handlers.ToArray()) handler.OnGameResumed();
    }

    public static void Register(IGamePauseHandler handler) => _handlers.Add(handler);
    public static void UnRegister(IGamePauseHandler handler) => _handlers.Remove(handler);

    private void Awake() => _input = new();

    private void OnEnable()
    {
        _input.Enable();
        _input.Main.Escape.performed += context =>
        {
            if (GameState.State == GameState.States.Playing) Pause();
            else if (GameState.State == GameState.States.Paused) Resume();
        };

        GameRestarter.Register(this);
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Main.Escape.performed -= context =>
        {
            if (GameState.State == GameState.States.Playing) Pause();
            else if (GameState.State == GameState.States.Paused) Resume();
        };

        GameRestarter.UnRegister(this);
    }
}