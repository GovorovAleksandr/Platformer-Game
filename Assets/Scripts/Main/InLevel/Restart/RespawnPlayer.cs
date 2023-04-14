using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RespawnPlayer : NetworkBehaviour, IGameRestartHandler
{
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    
    private const string RESPAWN_ANIMATION_KEY = "Respawn";

    public void OnGameRestarted()
    {
        _rigidbody.simulated = true;
        _renderer.enabled = true;
        transform.position = PlayerStartPos.Position;
        _animator.SetTrigger(RESPAWN_ANIMATION_KEY);
    }

    private void OnEnable() => GameRestarter.Register(this);
    private void OnDisable() => GameRestarter.UnRegister(this);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
}