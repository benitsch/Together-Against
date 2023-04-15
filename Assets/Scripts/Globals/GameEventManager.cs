using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerScoreReason
{
    None,
    OffScreen,
    HazardDeath,
    Conveyor,
    ReachedFinish
}
public delegate void OnPlayerScoreEventDelegate(int player1score, int player2score);
public delegate void OnFloatEventDelegate(int playerID, float newRemainingTime);
public delegate void OnGenericEventDelegate();
public class GameEventManager : Singleton<GameEventManager>
{
    private static GameEventManager instance;

    public OnPlayerScoreEventDelegate OnPlayerScoreEvent;
    public OnFloatEventDelegate OnPlayerReachedFinish;
    public OnGenericEventDelegate OnLevelTimeEnded;
    public OnGenericEventDelegate NotifyPlayLevelTransition;

    public bool Player1ReachedFinish = false;
    public bool Player2ReachedFinish = false;
    public int Player1Score = 0;
    public int Player2Score = 0;
    public int whoReachedFinishFirst = -1;

    public void LevelTimeEnded()
    {
        OnLevelTimeEnded?.Invoke();
        ClearDelegates();
    }

    public void ChangeLevel(int nextLevel)
    {
        ClearDelegates();
        Player1ReachedFinish = false;
        Player2ReachedFinish = false;
        SceneManager.LoadScene(nextLevel);
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
        OnPlayerScoreEvent?.Invoke(Player1Score, Player2Score);
    }

    public void PlayerReachedFinish(int playerID)
    {
        if(playerID == 0)
        {
            Player1ReachedFinish = true;
            if (whoReachedFinishFirst == -1)
            {
                whoReachedFinishFirst = 0;
            }
        }
        if(playerID == 1)
        {
            Player2ReachedFinish = true;
            if(whoReachedFinishFirst == -1)
            {
                whoReachedFinishFirst = 1;
            }
        }
        if(HaveBothFinishedLevel())
        {
            NotifyPlayLevelTransition?.Invoke();
        }
        else
        {
            OnPlayerReachedFinish?.Invoke(playerID, 60.0f);
        }
    }

    bool HaveBothFinishedLevel()
    {
        return Player1ReachedFinish && Player2ReachedFinish;
    }
}
