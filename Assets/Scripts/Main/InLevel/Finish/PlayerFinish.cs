using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFinish : NetworkBehaviour
{
    private Rigidbody2D _rigidbody;

    public void OnGameFinished()
    {
        _rigidbody.simulated = false;
        _rigidbody.velocity = Vector3.zero;

        if (isLocalPlayer) GameFinisher.Finish();
    }
    private void Start() => _rigidbody = GetComponent<Rigidbody2D>();
}