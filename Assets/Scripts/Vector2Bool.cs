using System;

[Serializable]
public struct Vector2Bool
{
    public bool x;
    public bool y;

    public Vector2Bool(bool x, bool y)
    {
        this.x = x;
        this.y = y;
    }
}