using System.Collections.Generic;
using UnityEngine;

public class PracticeMode : MonoBehaviour
{
    public static bool Enabled
    {
        get => _enabled;
        private set
        {
            _enabled = value;
            GameRestarter.Restart();
        }
    }
    private static bool _enabled = false;

    private static bool DesiredGameState => GameState.State == GameState.States.Paused;
    private static readonly List<IGameModeDependent> _normalModeHandlers = new List<IGameModeDependent>();

    private UserInput _input;

    public static void Register(IGameModeDependent handler) => _normalModeHandlers.Add(handler);
    public static void UnRegister(IGameModeDependent handler) => _normalModeHandlers.Remove(handler);

    public static void Disable()
    {
        foreach (var handler in _normalModeHandlers) handler.OnEnabledNormalMode();
        Enabled = false;
    }

    public static void Enable()
    {
        foreach (var handler in _normalModeHandlers) handler.OnEnabledPracticeMode();
        Enabled = true;
    }

    private static void ToggleMode()
    {
        if (!DesiredGameState) return;

        if (Enabled) Disable();
        else Enable();
    }


    private void Awake() => _input = new();

    private void OnEnable()
    {
        _input.Enable();
        _input.Main.TogglePracticeMode.performed += context => ToggleMode();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Main.TogglePracticeMode.performed -= context => ToggleMode();
    }
}
