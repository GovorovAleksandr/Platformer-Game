using System;
using UnityEngine;

[Serializable]
public struct ParallaxObject
{
    public Transform Transform;
    public float Speed;

    public float PosX { get; private set; }

    public void InitStartPosX() => PosX = Transform.position.x;
}