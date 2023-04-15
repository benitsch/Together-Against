using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerScoreReason
{
    None,
    OffScreen,
    HazardDeath,
    Conveyor,
    ReachedFinish
}
public delegate void OnPlayerEventDelegate(int playerID);
public delegate void OnFloatEventDelegate(int playerID, float newRemainingTime);
public delegate void OnGenericEventDelegate();
public class GameEventManager : Singleton<GameEventManager>
{


    private static GameEventManager instance;

    public OnPlayerEventDelegate OnPlayerScoreEvent;
    public OnPlayerEventDelegate OnPlayerReachedFinish;
    public OnGenericEventDelegate OnLevelTimeEnded;
    public OnGenericEventDelegate ChangeToNextLevel;

    public bool Player1ReachedFinish = false;
    public bool Player2ReachedFinish = false;
    public int Player1Score = 0;
    public int Player2Score = 0;

    public void LevelTimeEnded()
    {
        OnLevelTimeEnded?.Invoke();
        ClearDelegates();
    }

    private void ClearDelegates()
    {
        OnLevelTimeEnded = null;
        OnPlayerScoreEvent = null;
        OnPlayerReachedFinish = null;
    }
    public void PlayerScoreEvent(int playerID, PlayerScoreReason scoreReason)
    {
        int targetPlayerID = 0;
        switch (scoreReason)
        {
            case PlayerScoreReason.None:
                break;
            case PlayerScoreReason.OffScreen:
                targetPlayerID = 1 - playerID;
                if (playerID == 0)
                {
                    Player1Score = Player1Score + 1;
                }
                else
                {
                    Player2Score = Player2Score + 1;
                }
                break;
            case PlayerScoreReason.HazardDeath:
                targetPlayerID = 1 - playerID;
                if(playerID == 0)
                {
                    Player1Score = Player1Score + 1;
                }
                else
                {
                    Player2Score = Player2Score + 1;
                }
                break;
            case PlayerScoreReason.Conveyor:
                targetPlayerID = 1 - playerID;
                if (playerID == 0)
                {
                    Player1Score = Player1Score + 1;
                }
                else
                {
                    Player2Score = Player2Score + 1;
                }
                break;
        }
        OnPlayerScoreEvent?.Invoke(playerID);
    }

    public void PlayerReachedFinish(int playerID)
    {
        if(playerID == 0)
        {
            Player1ReachedFinish = true;
        }
        if(playerID == 1)
        {
            Player1ReachedFinish = true;
        }
        OnPlayerReachedFinish?.Invoke(playerID);
        if(Player1ReachedFinish && Player2ReachedFinish)
        {

        }
    }
}
