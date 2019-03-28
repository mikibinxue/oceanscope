using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyDirection : UnityEvent<bool, FlyDirection.Direction>
{
    public enum Direction
    {
        IDLE,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        FORWARD,
        BACK
    }
    new public void Invoke(bool isLeft, Direction flyDirection)
    {
        base.Invoke(isLeft, flyDirection);
        Current = flyDirection;
    }
    public Direction Current { get; private set; } = Direction.IDLE;

}
