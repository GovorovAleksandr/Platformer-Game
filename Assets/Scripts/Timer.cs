using System.Collections;
using UnityEngine;
using System;

public class Timer
{
    public bool IsOver { get; private set; }
    public event Action<float> TimeLeftChanged;

    public const float UPDATE_STEP = 0.1f;
    private float _timeLeft;
    private GameState.States State => GameState.State;
    private bool DesiredGameState => State == GameState.States.Playing;

    public IEnumerator Restart(float duration)
    {
        IsOver = false;
        _timeLeft = duration;

        while (_timeLeft >= 0)
        {
            yield return new WaitUntil(() => DesiredGameState);

            _timeLeft -= duration * UPDATE_STEP;
            TimeLeftChanged?.Invoke(_timeLeft);
            yield return new WaitForSecondsRealtime(duration * UPDATE_STEP);
        }

        IsOver = true;
    }
}