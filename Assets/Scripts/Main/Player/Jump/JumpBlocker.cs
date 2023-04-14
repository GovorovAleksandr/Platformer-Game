using System;
using UnityEngine;
using NaughtyAttributes;
using Mirror;

public class JumpBlocker : NetworkBehaviour
{
    [SerializeField] private bool _useCooldown;
    [ShowIf(nameof(_useCooldown))]
    [SerializeField] private float _cooldown;
    [SerializeField] PlayerGravity playerGravitation;

    public static event Action<float> ExpiredTimeChanged;

    private Timer _timer;

    private bool DesiredGameState => GameState.State == GameState.States.Playing;

    public void Restart() => StartCoroutine(_timer.Restart(_cooldown));
    public void OnTimeLeftChanged(float timeLeft) => ExpiredTimeChanged?.Invoke(timeLeft / _cooldown);

    private void Awake()
    {
        _timer = new();
        Restart();
    }

    public bool Allow()
    {
        if (!DesiredGameState || !playerGravitation.UseGravity) return false;

        if (_useCooldown)
        {
            if (!_timer.IsOver) return false;
            Restart();
        }
        return true;
    }

    private void OnEnable() => _timer.TimeLeftChanged += OnTimeLeftChanged;
    private void OnDisable() => _timer.TimeLeftChanged -= OnTimeLeftChanged;
}
