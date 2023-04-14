using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class PlayerTrail : MonoBehaviour, ITimeRewindHandler, IGameDefeatHandler, IGameFinishHandler, IGameRestartHandler
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _length;
    [SerializeField] private List<Vector3> _points;

    private LineRenderer _trail;

    private bool DesiredGameState => GameState.State == GameState.States.Playing;

    public void ApplyPointInTime(object data)
    {
        _points = (List<Vector3>)data;

        _trail.positionCount = _points.Count;
        _trail.SetPositions(_points.ToArray());
    }

    public object GetPointInTime()
    {
        List<Vector3> points = new List<Vector3>();

        foreach(Vector3 point in _points)
            points.Add(point);

        return points;
    }

    public void Defeat() => ResetTrail();
    public void OnGameFinished() => ResetTrail();
    public void OnGameRestarted() => ResetTrail();

    private void FixedUpdate()
    {
        if (!DesiredGameState || TimeRewinder.IsRewinding) return;

        if (_points.Count >= _length) _points.RemoveAt(0);

        _points.Add(_target.position);
        SyncTrail();
    }

    private void ResetTrail()
    {
        _points.Clear();
        SyncTrail();
    }

    private void SyncTrail()
    {
        _trail.positionCount = _points.Count;
        _trail.SetPositions(_points.ToArray());
    }

    private void Start() => _trail = GetComponent<LineRenderer>();

    private void OnEnable()
    {
        TimeRewinder.Register(this);
        GameDefeater.Register(this);
        TimeRewinder.Register(this);
        GameRestarter.Register(this);
    }

    private void OnDisable()
    {
        TimeRewinder.UnRegister(this);
        GameDefeater.UnRegister(this);
        TimeRewinder.UnRegister(this);
        GameRestarter.UnRegister(this);
    }
}
