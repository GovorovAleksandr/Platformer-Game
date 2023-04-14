using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class JumpHandler : NetworkBehaviour
{
    [SerializeField] protected SurfaceCheck GroundCheck;

    [Min(0)] [SerializeField] private float _force;

    protected bool Allowed => _jumpBlocker.Allow();

    private JumpBlocker _jumpBlocker;
    private Rigidbody2D _rigidbody;

    protected void Jump()
    {
        _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(-Physics2D.gravity.normalized * _force, ForceMode2D.Impulse);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _jumpBlocker = GetComponent<JumpBlocker>();
    }
}
