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
    public float timeWhenFirstPlayerReachedEnd = 0;
    public float endgameTime = 60f;
    public void LevelTimeEnded()
    {
        OnLevelTimeEnded?.Invoke();
        ClearDelegates();
    }

    public void ChangeLevel(int nextLevel)
    {
        if(Player1ReachedFinish)
        {
            Player1Score = Player1Score + 3;
        }
        if(Player2ReachedFinish)
        {
            Player2Score = Player2Score + 3;
        }
        if(whoReachedFinishFirst != -1)
        {
            float timeLeft = Time.timeSinceLevelLoad - timeWhenFirstPlayerReachedEnd;
            
            int score = (int)(endgameTime - timeLeft);

            Debug.Log("Time left : " + timeLeft + ", score time : " + score);

            if (whoReachedFinishFirst == 0)
            {
                Player1Score += score;
            }
            else if (whoReachedFinishFirst == 1)
            {
                Player2Score += score;
            }
        }
        ClearDelegates();
        whoReachedFinishFirst = -1;
        timeWhenFirstPlayerReachedEnd = 0;
        Player1ReachedFinish = false;
        Player2ReachedFinish = false;
        SceneManager.LoadScene(nextLevel);
    }

    public void FullReset()
    {
        ClearDelegates();
        whoReachedFinishFirst = -1;
        timeWhenFirstPlayerReachedEnd = 0;
        Player1ReachedFinish = false;
        Player2ReachedFinish = false;
        Player1Score = 0;
        Player2Score = 0;
    }

    private void ClearDelegates()
    {
        OnLevelTimeEnded = null;
        OnPlayerScoreEvent = null;
        OnPlayerReachedFinish = null;
    }
    public void PlayerScoreEvent(int playerID, PlayerScoreReason scoreReason)
    {
        switch (scoreReason)
        {
            case PlayerScoreReason.None:
                break;
            case PlayerScoreReason.OffScreen:
                if (playerID == 0)
                {
                    Player2Score = Player1Score + 1;
                }
                else
                {
                    Player1Score = Player1Score + 1;
                }
                break;
            case PlayerScoreReason.HazardDeath:
                if(playerID == 0)
                {
                    Player2Score = Player2Score + 1;
                }
                else
                {
                    Player1Score = Player1Score + 1;
                }
                break;
            case PlayerScoreReason.Conveyor:
                if (playerID == 0)
                {
                    Player2Score = Player2Score + 1;
                }
                else
                {
                    Player1Score = Player1Score + 1;
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
        else if(playerID == 1)
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
            timeWhenFirstPlayerReachedEnd = Time.timeSinceLevelLoad;
            OnPlayerReachedFinish?.Invoke(playerID, endgameTime);
        }
    }

    bool HaveBothFinishedLevel()
    {
        return Player1ReachedFinish && Player2ReachedFinish;
    }
}
