using UnityEngine;

public class PlayerStartPos : MonoBehaviour
{
    public static Vector2 Position;

    private void Awake() => Position = transform.position;
}
