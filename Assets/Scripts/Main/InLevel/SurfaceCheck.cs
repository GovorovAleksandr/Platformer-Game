using UnityEngine;
using System;

[DisallowMultipleComponent]
public class SurfaceCheck : MonoBehaviour
{
    public bool OnSurface
    {
        get => _onSurface;
        private set => _onSurface = value;
    }
    [SerializeField] private bool _onSurface;
    public event Action OnSurfaceEvent;

    [SerializeField] private LayerMask _surfaceLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Physics2D.IsTouchingLayers(GetComponent<Collider2D>(), _surfaceLayer)) return;

        OnSurface = true;
        OnSurfaceEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision) => OnSurface = false;
}