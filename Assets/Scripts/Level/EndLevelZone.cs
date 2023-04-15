using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelZone : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (!pc)
        {
            return;
        }
        GameEventManager.Instance.PlayerReachedFinish(pc.playerID);
        pc.movementControlsLocked = true;
    }
}
