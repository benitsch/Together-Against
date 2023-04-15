using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnPickedUpDelegate(Pickup pickedUpItem);
public delegate void OnDroppedDelegate(Pickup pickup, DropType dropType);
public enum DropType
{
    None,
    Dropped,
    Thrown,
    BrokeFree
}
public class Pickup : MonoBehaviour
{
    public OnPickedUpDelegate OnPickedUp;
    public OnDroppedDelegate OnNoLongerPickedUp;

    [ReadOnly, SerializeField] private PlayerController pickedUpBy = null;
    Rigidbody2D body = null;

    PlayerController myPc;

    public Transform bottomLocation = null;
    TargetJoint2D joint = null;
    public Collider2D coll { get; private set; } = null;

    public float maxForce = 1000;
    public float forceBreak = 1000;
    private void Awake()
    {
        myPc = GetComponent<PlayerController>();
        coll = GetComponent<Collider2D>();
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
        //joint.connectedBody = pc.GetComponent<Rigidbody2D>();
        joint.target = pc.pickupSlotLocation.position;
        //joint.enableCollision = false;
        coll.enabled = false;
        joint.maxForce = maxForce;
        joint.breakForce = forceBreak;
        CancelInvoke();
        Invoke("ReenableCollision", 1f);
    }

    private void OnJointBreak2D(Joint2D joint)
    {
        FinalizeDropped(DropType.BrokeFree);
    }

    public void ReenableCollision()
    {
        coll.enabled = true;
    }

    public bool Drop(PlayerController pc, Vector2 dropLocation)
    {
        joint.target = dropLocation;
        CancelInvoke("ReenableCollision");
        Invoke("PostDropAnimation", 0.5f);

        return true;
    }

    public void BreakFree()
    {
        CancelInvoke("ReenableCollision");
        FinalizeDropped(DropType.BrokeFree);
    }

    internal void Throw(Vector2 throwVelocity)
    {
        body.velocity = throwVelocity;
        FinalizeDropped(DropType.Thrown);
    }

    void PostDropAnimation()
    {
        FinalizeDropped(DropType.Dropped);
    }
    void FinalizeDropped(DropType dropType)
    {
        CancelInvoke("FinalizeDropped");
        if (myPc)
        {
            myPc.pickedUpItem = null;
        }
        pickedUpBy.pickedUpItem = null;
        pickedUpBy = null;
        coll.enabled = true;
        GameObject.Destroy(joint);
        joint = null;
        OnNoLongerPickedUp?.Invoke(this, dropType);
    }
}
