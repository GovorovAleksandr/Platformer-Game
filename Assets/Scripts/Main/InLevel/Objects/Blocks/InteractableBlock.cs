using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableBlock : MonoBehaviour, IGameRestartHandler, ITimeRewindHandler
{
    [SerializeField] private RigidbodyConstraints2D _freezePosition;
    private Rigidbody2D _rigidbody;
    private Vector2 _startPos;

    public object GetPointInTime() => new ObjectProperties(transform.position, transform.rotation, _rigidbody.velocity);
    public void ApplyPointInTime(object data)
    {
        ObjectProperties positionAndRotation = (ObjectProperties)data;
        transform.SetPositionAndRotation(positionAndRotation.Position, positionAndRotation.Rotation);
        _rigidbody.velocity = positionAndRotation.Velocity;
    }

    public void OnGameRestarted()
    {
        transform.position = _startPos;
        _rigidbody.velocity = Vector2.zero;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
    }

    private void OnValidate()
    {
        if (_rigidbody == null) return;
        _rigidbody.constraints = _freezePosition;
    }

    private void OnEnable()
    {
        TimeRewinder.Register(this);
        GameRestarter.Register(this);
    }

    private void OnDisable()
    {
        TimeRewinder.UnRegister(this);
        GameRestarter.UnRegister(this);
    }
}
