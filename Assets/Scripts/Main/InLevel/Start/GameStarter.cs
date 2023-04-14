using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private static readonly List<IGameStartHandler> _handlers = new();

    public static void StartGame()
    {
        foreach (var handler in _handlers.ToArray()) handler.OnGameStarted();
    }

    public static void Register(IGameStartHandler handler) => _handlers.Add(handler);
    public static void UnRegister(IGameStartHandler handler) => _handlers.Remove(handler);
}