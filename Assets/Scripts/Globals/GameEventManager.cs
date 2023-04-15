using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnPlayerEventDelegate(int playerID);
public class GameEventManager : Singleton<GameEventManager>
{
    private static GameEventManager instance;

    OnPlayerEventDelegate OnPlayerDeath;
    OnPlayerEventDelegate OnPlayerReachedFinish;

    public void PlayerDied(int playerID)
    {
        OnPlayerDeath?.Invoke(playerID);
    }

    public void PlayerReachedFinish(int playerID)
    {
        OnPlayerReachedFinish?.Invoke(playerID);
    }
}
