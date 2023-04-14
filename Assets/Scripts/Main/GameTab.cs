using UnityEngine;

public class GameTab : MonoBehaviour
{
    public void Open() => TabsList.Open(gameObject);
    public void Close() => TabsList.CloseLast();
}