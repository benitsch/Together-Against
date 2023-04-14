using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnPickedUpDelegate(Pickup pickedUpItem);
public delegate void OnDroppedDelegate(Pickup pickup, DropType dropType);
public enum DropType
{
    Dropped,
    BrokeFree
}
public class Pickup : MonoBehaviour
{
    public OnPickedUpDelegate OnPickedUp;
    public OnDroppedDelegate OnDropped;

    [ReadOnly, SerializeField] private PlayerController pickedUpBy = null;
    Rigidbody2D body = null;

    public Transform bottomLocation = null;
    TargetJoint2D joint = null;
    Collider2D collider = null;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        body = GetComponent<Rigidbody2D>();
    }
    public bool TryPickup(PlayerController carrier)
    {
        if(pickedUpBy == null)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (pickedUpBy != null)
        {
            joint.target = pickedUpBy.pickupSlotLocation.position - bottomLocation.localPosition;
        }
    }

    public virtual void PickedUp(PlayerController pc)
    {
        pickedUpBy = pc;
        OnPickedUp?.Invoke(this);

        joint = gameObject.AddComponent<TargetJoint2D>();
        joint.connectedBody = pc.GetComponent<Rigidbody2D>();
        joint.target = pc.pickupSlotLocation.position;
        joint.enableCollision = false;
        collider.enabled = false;
        Invoke("ReenableCollision", 0.5f);
    }

    private void OnJointBreak2D(Joint2D joint)
    {
        DropInternal(null, DropType.BrokeFree);
        FinalizeDropped();
    }

    public void ReenableCollision()
    {
        collider.enabled = true;
    }

    public bool Drop(PlayerController pc)
    {
        if(pickedUpBy != pc)
        {
            return false;
        }
        return DropInternal(pc, DropType.Dropped);
    }

    protected bool DropInternal(PlayerController pc, DropType dropType)
    {
        pickedUpBy = null;
        if (dropType == DropType.Dropped)
        {
            CancelInvoke("ReenableCollision");
            collider.enabled = false;
            joint.target = pc.pickupScanLocation.position - bottomLocation.localPosition;
            Invoke("FinalizeDropped", 0.5f);
        }
        else if(dropType == DropType.BrokeFree)
        {
            FinalizeDropped();
        }
        return true;
    }

    void FinalizeDropped()
    {
        CancelInvoke("FinalizeDropped");
        pickedUpBy = null;
        collider.enabled = true;
        GameObject.Destroy(joint);
        joint = null;
    }
}
