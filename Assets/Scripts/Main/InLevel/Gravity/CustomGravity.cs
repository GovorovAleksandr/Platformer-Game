using UnityEngine;
using System.Collections.Generic;

public class CustomGravity : MonoBehaviour, IGameRestartHandler
{
    [SerializeField]
    [Range(-180f, 180f)]
    private float _gravityAngle;

    private static readonly List<IChangeGravityHandler> handlers = new();

    private static Vector2 _defaultDirection;

    public static void Rotate(float angle)
    {
        Physics2D.gravity = Quaternion.Euler(0, 0, angle) * _defaultDirection;
        LocalPlayerReference.PlayerGravity.RotatePlayer(angle);
    }

    public void OnGameRestarted() => ResetGravityDirection();

    public static Vector3 GetGravityPerpendicular(float axis)
    {
        Vector3 perpendicular = Vector2.Perpendicular(Physics2D.gravity).normalized;
        return perpendicular * axis;
    }

    public static Vector3 GetGravityAlong(float axis) => -Physics2D.gravity.normalized * axis;

    public static void Register(IChangeGravityHandler handler) => handlers.Add(handler);
    public static void UnRegister(IChangeGravityHandler handler) => handlers.Remove(handler);

    private void ResetGravityDirection() => Physics2D.gravity = _defaultDirection;

    private void Awake() => _defaultDirection = Physics2D.gravity;
    private void OnEnable() => GameRestarter.Register(this);
    private void OnDisable() => GameRestarter.UnRegister(this);
}