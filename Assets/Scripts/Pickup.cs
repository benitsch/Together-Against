using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnPickedUpDelegate(Pickup pickedUpItem);

public class Pickup : MonoBehaviour
{
    OnPickedUpDelegate OnPickedUp;
    Transform pickedUpBy = null;

    public Transform bottomLocation = null;
    public bool TryPickup(Carrier carrier)
    {
        if(pickedUpBy == null)
        {
            OnPickedUp?.Invoke(this);
            return true;
        }
        return false;
    }
    protected virtual void PickedUp()
    {

    }
}
