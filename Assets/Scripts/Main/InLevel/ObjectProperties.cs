using System;
using UnityEngine;

public readonly struct ObjectProperties
{
    public readonly Vector3 Position; 
    public readonly Quaternion Rotation;
    public readonly Vector2 Velocity;
    public readonly Transform Transform;
    public readonly Vector2 StartPos;

    public float SpriteWidth => _spriteRenderer.bounds.size.x;
    public float SpriteHeight => _spriteRenderer.bounds.size.y;

    private readonly SpriteRenderer _spriteRenderer;

    public ObjectProperties(Vector3 position, Quaternion rotation, Vector2 velocity)
    {
        Position = position;
        Rotation = rotation;
        Velocity = velocity;

        Transform = null;
        StartPos = Vector2.zero;
        _spriteRenderer = null;
    }

    public ObjectProperties(Vector3 position)
    {
        Position = position;
        Rotation = Quaternion.identity;

        Velocity = Vector2.zero;
        Transform = null;
        StartPos = Vector2.zero;
        _spriteRenderer = null;
    }

    public ObjectProperties(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;

        Velocity = Vector2.zero;
        Transform = null;
        StartPos = Vector2.zero;
        _spriteRenderer = null;
    }

    public ObjectProperties(Transform transform, Vector2 startPos, SpriteRenderer spriteRenderer)
    {
        Position = Vector3.zero;
        Rotation = Quaternion.identity;
        Velocity = Vector2.zero;

        Transform = transform;
        StartPos = startPos;
        _spriteRenderer = spriteRenderer;
    }
}