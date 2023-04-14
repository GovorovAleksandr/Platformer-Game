using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerGravity : MonoBehaviour, IChangeGravityHandler, IGameStartHandler, IGameRestartHandler
{
    public bool UseGravity => _useGravity;

    private bool _useGravity;

    [ShowIf(nameof(UseGravity))]
    [SerializeField] private float _gravityScale;
    
    private Rigidbody2D _rigidbody;

    public void RotatePlayer(float angle)
    {
        ResetVelocity();

        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, angle);
    }

    public void OnGameRestarted() => ReturnDefaultGravity();
    public void OnGameStarted() => ReturnDefaultGravity();
    public void Enable()
    {
        _useGravity = true;
        ResetVelocity();
        _rigidbody.gravityScale = _gravityScale;
    }
    public void Disable()
    {
        _useGravity = false;
        _rigidbody.gravityScale = 0;
    }
    public void OnGravityChanged(float gravityRotation) => RotatePlayer(gravityRotation);
    private void ResetVelocity() => _rigidbody.velocity = Vector2.zero;
    private void ResetPlayerRotation() => transform.rotation = Quaternion.identity;
    private void Start() => _rigidbody = GetComponent<Rigidbody2D>();

    private void ReturnDefaultGravity()
    {
        ResetPlayerRotation();
        Enable();
    }

    private void OnEnable()
    {
        CustomGravity.Register(this);
        GameRestarter.Register(this);
        GameStarter.Register(this);
    }
    private void OnDisable()
    {
        CustomGravity.UnRegister(this);
        GameRestarter.UnRegister(this);
        GameStarter.UnRegister(this);
    }
}