using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnPickedUpDelegate(Pickup pickedUpItem);

struct SavedRigidBodyProperties
{
    public RigidbodyType2D type;
}
public class Pickup : MonoBehaviour
{
    OnPickedUpDelegate OnPickedUp;
    PlayerController pickedUpBy = null;
    Rigidbody2D body = null;

    public Transform bottomLocation = null;

    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    public bool TryPickup(PlayerController carrier)
    {
        if(pickedUpBy == null)
        {
            pickedUpBy = carrier;
            OnPickedUp?.Invoke(this);
            return true;
        }
        return false;
    }

    protected virtual void PickedUp()
    {

    }
}
