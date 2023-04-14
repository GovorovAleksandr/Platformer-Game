using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    private static readonly List<IGameFinishHandler> _finishGames = new List<IGameFinishHandler>();

    public static void Finish()
    {
        foreach (IGameFinishHandler finishGame in _finishGames) finishGame.OnGameFinished();
    }

    public static void Register(IGameFinishHandler finishGame) => _finishGames.Add(finishGame);
    public static void UnRegister(IGameFinishHandler finishGame) => _finishGames.Remove(finishGame);
}
