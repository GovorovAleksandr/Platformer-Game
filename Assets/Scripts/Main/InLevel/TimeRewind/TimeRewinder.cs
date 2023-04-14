using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeRewinder : MonoBehaviour, IGameFinishHandler
{
    public static event Action<bool> Rewinding;
    public static event Action<float> RewindingProgress;
    public static bool IsRewinding { get; private set; }

    [SerializeField] private float _secondsForRecord;
    public float SecondsForRecord => _secondsForRecord;

    private readonly List<List<object>> _pointsInTime = new List<List<object>>();
    private static readonly List<ITimeRewindHandler> _timeDependents = new List<ITimeRewindHandler>();

    private bool DesiredGameState => GameState.State == GameState.States.Playing && PracticeMode.Enabled;

    private UserInput _input;

    private void FixedUpdate()
    {
        if (!DesiredGameState) return;

        if (IsRewinding) Rewind();
        else Record();
        RewindingProgress?.Invoke(1 - _secondsForRecord / _pointsInTime.Count);
    }

    private void Rewind()
    {
        if (_pointsInTime.Count > 0)
        {
            var pointInTime = _pointsInTime[_pointsInTime.Count - 1];

            for(int i = 0; i < pointInTime.Count; i++)
                if (pointInTime != null)
                    _timeDependents[i].ApplyPointInTime(pointInTime[i]);

            _pointsInTime.Remove(pointInTime);
        }
        else StopRewinding();
    }

    private void Record()
    {
        if (_pointsInTime.Count > _secondsForRecord / Time.fixedDeltaTime - 1)
            _pointsInTime.RemoveAt(0);

        var pointInTime = new List<object>();

        foreach (var data in _timeDependents) pointInTime.Add(data.GetPointInTime());

        _pointsInTime.Add(pointInTime);
    }

    public void StartRewinding()
    {
        if (!DesiredGameState) return;
        Rewinding?.Invoke(IsRewinding = true);
    }
    public void StopRewinding()
    {
        if (!DesiredGameState) return;
        Rewinding?.Invoke(IsRewinding = false);
    }

    public void OnGameFinished() => _pointsInTime.Clear();

    public static void Register(ITimeRewindHandler timeDependent) => _timeDependents.Add(timeDependent);
    public static void UnRegister(ITimeRewindHandler timeDependent) => _timeDependents.Remove(timeDependent);

    private void Awake() => _input = new();

    private void OnEnable()
    {
        GameFinisher.Register(this);

        _input.Enable();
        _input.Main.TimeRewind.performed += context => StartRewinding();
        _input.Main.TimeRewind.canceled += context => StopRewinding();
    }

    private void OnDisable()
    {
        GameFinisher.UnRegister(this);

        _input.Disable();
        _input.Main.TimeRewind.performed -= context => StartRewinding();
        _input.Main.TimeRewind.canceled -= context => StopRewinding();
    }
}
