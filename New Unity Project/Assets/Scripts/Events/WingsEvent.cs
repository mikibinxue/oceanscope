using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WingsEvent : UnityEvent<bool, WingsEvent.WingStatus>
{
    public WingsEvent(int nCapacity = 3)
    {
        m_capacity = nCapacity;
    }
    private int m_capacity;

    public enum WingStatus
    {
        IDLE,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        FORWARD,
        BACK
    }
    public Queue<WingStatus> LeftQueue = new Queue<WingStatus>(); //{ get; private set; }
    public Queue<WingStatus> RightQueue = new Queue<WingStatus>(); //{ get; private set; }

    new public void Invoke(bool isLeft, WingStatus wingStatus)
    {
        if (isLeft)
        {
            LeftQueue.Enqueue(wingStatus);
            if (LeftQueue.Count > m_capacity)
                LeftQueue.Dequeue();
        }
        else
        {
            RightQueue.Enqueue(wingStatus);
            if (RightQueue.Count > m_capacity)
                RightQueue.Dequeue();
        }
        base.Invoke(isLeft, wingStatus);
    }

}
