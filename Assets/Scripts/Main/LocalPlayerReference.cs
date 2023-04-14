using UnityEngine;

public class LocalPlayerReference : MonoBehaviour
{
    public static Transform Transform => _transform;
    public static PlayerGravity PlayerGravity => _playerGravity;

    private static Transform _transform;
    private static PlayerGravity _playerGravity;

    public static void SetPlayer(Transform player)
    {
        _transform = player;
        player.TryGetComponent(out _playerGravity);
    }
}