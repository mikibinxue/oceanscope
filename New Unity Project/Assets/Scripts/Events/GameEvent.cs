using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : UnityEvent<GameEvent.GameStatus>
{
    public enum GameStatus
    {
        INIT,
        CALIBRATING,
        PLAY
    }
    new public void Invoke(GameStatus gameStatus)
    {
        base.Invoke(gameStatus);
        Last = Current;
        Current = gameStatus;
    }
    public GameStatus Last { get; private set; } = GameStatus.INIT;
    public GameStatus Current { get; private set; } = GameStatus.INIT;
}
