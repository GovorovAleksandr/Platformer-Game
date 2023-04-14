using UnityEngine;
using System.Collections.Generic;

public class GameRestarter : MonoBehaviour
{
    private static readonly List<IGameRestartHandler> _handlers = new();
    private UserInput _input;

    public static void Restart()
    {
        foreach (var handler in _handlers.ToArray()) handler.OnGameRestarted();
    }

    public static void Register(IGameRestartHandler handler) => _handlers.Add(handler);
    public static void UnRegister(IGameRestartHandler handler) => _handlers.Remove(handler);

    private void Awake() => _input = new();

    private void OnEnable()
    {
        _input.Enable();
        _input.Main.Restart.performed += context => Restart();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Main.Restart.performed -= context => Restart();
    }
}