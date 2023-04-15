using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerScoreReason
{
    None,
    OffScreen,
    HazardDeath,
    Conveyor,
    EndLevelTimer
}
public delegate void OnPlayerEventDelegate(int playerID);
public class GameEventManager : Singleton<GameEventManager>
{


    private static GameEventManager instance;

    OnPlayerEventDelegate OnPlayerDeath;
    OnPlayerEventDelegate OnPlayerReachedFinish;

    public int Player1Score = 0;
    public int Player2Score = 0;

    public void PlayerScoreEvent(int playerID, PlayerScoreReason scoreReason)
    {
        OnPlayerDeath?.Invoke(playerID);
    }

    public void PlayerReachedFinish(int playerID)
    {
        OnPlayerReachedFinish?.Invoke(playerID);
    }
}
