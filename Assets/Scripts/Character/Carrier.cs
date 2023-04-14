using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    PlayerController pc;

    Pickup pickedUp = null;

    public Transform pickupSlotLocation;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        if(pc)
        {

        }
    }

    void NotifyUseKeyPressed()
    {

    }
}
