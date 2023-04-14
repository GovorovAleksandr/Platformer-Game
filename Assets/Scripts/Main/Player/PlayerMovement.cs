using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : NetworkBehaviour, IGamePauseHandler, IGameRestartHandler, ITimeRewindHandler
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedWithoutGravity;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlayerGravity _playerGravity;

    private DirectionState _currentDirection;
    private Rigidbody2D _rigidbody;

    private enum DirectionState
    {
        Right,
        Left
    }

    private UserInput _input;
    private Vector2 _direction;

    private bool DesiredGameState => GameState.State == GameState.States.Playing;

    public void OnGamePaused() => _rigidbody.simulated = false;
    public void OnGameResumed() => _rigidbody.simulated = true;
    public void OnGameRestarted() => _rigidbody.velocity = Vector2.zero;

    public void ApplyPointInTime(object data)
    {
        var dataTransform = (ObjectProperties)data;
        transform.position = dataTransform.Position;
    }

    public object GetPointInTime() => new ObjectProperties(transform.position);
    private void Rewinding(bool rewinding) => _rigidbody.simulated = !rewinding;

    private void FixedUpdate()
    {
        if (!DesiredGameState || !isLocalPlayer) return;
        Move(_direction);
    }

    private void Move(Vector2 direction)
    {
        float multiplier = (_playerGravity.UseGravity ? _speed : _speedWithoutGravity) * Time.fixedDeltaTime;

        Vector2 directionWithGravity = CustomGravity.GetGravityPerpendicular(direction.x);
        Vector2 directionAlongGravity = _playerGravity.UseGravity ? Vector2.zero : CustomGravity.GetGravityAlong(direction.y);

        Vector2 offset = directionWithGravity;
        if (!_playerGravity.UseGravity) offset += directionAlongGravity;
        _rigidbody.position += offset * multiplier;
    }

    private void OnMovePerformed(InputAction.CallbackContext context) => _direction = context.ReadValue<Vector2>();
    private void OnMoveCanceled(InputAction.CallbackContext context) => _direction = Vector2.zero;

    private void OnEnable()
    {
        GameSetPause.Register(this);
        TimeRewinder.Register(this);

        TimeRewinder.Rewinding += Rewinding;

        _input.Enable();
        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        GameSetPause.UnRegister(this);
        TimeRewinder.UnRegister(this);

        TimeRewinder.Rewinding -= Rewinding;

        _input.Disable();
        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Move.canceled -= OnMoveCanceled;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = new();
    }
}