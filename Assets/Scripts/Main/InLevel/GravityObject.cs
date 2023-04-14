using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityObject : MonoBehaviour
{
    [SerializeField] private Vector2 gravityDirection = new(1, 0);

    private Rigidbody2D _rigidbody;

    private void Start() => _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody.position;
        Vector2 velocity = _rigidbody.velocity;

        float gravity = 9.81f * _rigidbody.gravityScale;
        velocity += gravityDirection * (gravity * Time.fixedDeltaTime);

        position += velocity * Time.fixedDeltaTime;

        _rigidbody.MovePosition(position);
        _rigidbody.velocity = velocity;
    }
}