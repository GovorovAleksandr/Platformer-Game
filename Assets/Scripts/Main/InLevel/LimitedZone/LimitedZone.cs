using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(ZoneDrawer))]
public class LimitedZone : MonoBehaviour
{
    [SerializeField] private Vector2Bool _limitMin;
    [SerializeField] private Vector2Bool _limitMax;
    [SerializeField] private Vector2 _min;
    [SerializeField] private Vector2 _max;

    [SerializeField][Range(-1f, 1f)] private float _hitboxDifference;

    public Vector2Bool LimitMin => _limitMin;
    public Vector2Bool LimitMax => _limitMax;
    public Vector3 Min => _min;
    public Vector3 Max => _max;

    public event Action<Collider2D> TriggerEntered;
    public event Action<Collider2D> TriggerCameOut;
    
    private BoxCollider2D _boxCollider;
    private ZoneDrawer _drawer;

    public Vector3 GetSize() => new(GetWidth(), GetHeight());
    public Vector3 GetOffset() => new(Min.x + (GetWidth() / 2) - 0.5f, Min.y + (GetHeight() / 2) - 0.5f);
    public float GetWidth() => Max.x - Min.x;
    public float GetHeight() => Max.y - Min.y;

    private void Awake()
    {
        FindCollider();
        FindDrawer();
    }

    private void FindCollider() => _boxCollider = GetComponent<BoxCollider2D>();
    private void FindDrawer() => _drawer = GetComponent<ZoneDrawer>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEntered?.Invoke(collision);

        if (collision.GetComponent<PlayerMovement>()) _drawer.Show();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerCameOut?.Invoke(collision);

        if (collision.GetComponent<PlayerMovement>()) _drawer.Hide();
    }

    private void OnValidate()
    {
        if (_drawer == null) FindDrawer();
        if(_boxCollider == null) FindCollider();

        float sizeX = GetSize().x == 0 ? 1f : GetSize().x;
        float sizeY = GetSize().y == 0 ? 1f : GetSize().y;
        Vector3 size = new(sizeX, sizeY);

        _drawer.ReDraw(GetOffset(), size);

        Vector2 colliderSize = new(sizeX - _hitboxDifference, sizeY - _hitboxDifference);

        _boxCollider.offset = GetOffset();
        _boxCollider.size = colliderSize;
    }
}