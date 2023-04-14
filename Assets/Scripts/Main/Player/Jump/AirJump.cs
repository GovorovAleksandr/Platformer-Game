using System;
using UnityEngine;

[DisallowMultipleComponent]
public class AirJump : JumpHandler, IGameRestartHandler, ITimeRewindHandler
{
    [SerializeField] private SurfaceCheck _wallCheck;
    [Min(0)] [SerializeField] private int _maxJumpsCount;

    public event Action<int> JumpsLeftChanged;

    public int JumpsLeft
    {
        get => _jumpsLeft;
        private set
        {
            _jumpsLeft = value;
            JumpsLeftChanged?.Invoke(value);
        }
    }

    private int _jumpsLeft;
    private UserInput _input;

    public void TryJump()
    {
        if (JumpsLeft <= 0 || GroundCheck.OnSurface || _wallCheck.OnSurface || !Allowed || !isLocalPlayer) return;

        Jump();
        JumpsLeft--;
    }

    public void OnGameRestarted() => ResetUsedAirJumps();
    public void ApplyPointInTime(object data) => JumpsLeft = (int)data;
    public object GetPointInTime() => JumpsLeft;

    private void ResetUsedAirJumps() => JumpsLeft = _maxJumpsCount;

    private void Awake() => _input = new();
    private void OnEnable()
    {
        GroundCheck.OnSurfaceEvent += ResetUsedAirJumps;
        GameRestarter.Register(this);
        TimeRewinder.Register(this);

        _input.Enable();
        _input.Player.Jump.performed += context => TryJump();
    }

    private void OnDisable()
    {
        GroundCheck.OnSurfaceEvent -= ResetUsedAirJumps;
        GameRestarter.UnRegister(this);
        TimeRewinder.UnRegister(this);

        _input.Disable();
        _input.Player.Jump.performed -= context => TryJump();
    }
}
