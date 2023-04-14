using Mirror;
using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerDefeat : NetworkBehaviour
{
    public event Action Defeated;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    public void Defeat()
    {
        _renderer.enabled = false;
        _rigidbody.simulated = false;
        _rigidbody.velocity = Vector3.zero;
        Defeated?.Invoke();

        if (isLocalPlayer) GameDefeater.Defeat();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
}
