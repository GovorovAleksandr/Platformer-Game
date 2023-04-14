using System.Collections.Generic;
using UnityEngine;

public class GameDefeater : MonoBehaviour
{
    private static readonly List<IGameDefeatHandler> _handlers = new List<IGameDefeatHandler>();

    public static void Defeat()
    {
        foreach (var handler in _handlers.ToArray()) handler.Defeat();
    }

    public static void Register(IGameDefeatHandler handler) => _handlers.Add(handler);
    public static void UnRegister(IGameDefeatHandler handler) => _handlers.Remove(handler);
}
