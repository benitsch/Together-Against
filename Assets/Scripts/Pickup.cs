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
    public Collider2D coll { get; private set; } = null;

    public float maxForce = 1000;
    public float forceBreak = 1000;
    private void Awake()
    {
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
        joint.breakForce = gameObject.CompareTag("Player") ? 1000 : 1000;
        joint.maxForce = maxForce;
        Invoke("ReenableCollision", 1f);
    }

    private void OnJointBreak2D(Joint2D joint)
    {
        DropInternal(null, DropType.BrokeFree);
        FinalizeDropped();
    }

    public void ReenableCollision()
    {
        coll.enabled = true;
    }

    public bool Drop(PlayerController pc, Vector2 dropLocation)
    {
        if(pickedUpBy != pc)
        {
            return false;
        }
        joint.target = dropLocation;
        return DropInternal(pc, DropType.Dropped);
    }

    protected bool DropInternal(PlayerController pc, DropType dropType)
    {
        pickedUpBy = null;
        if (dropType == DropType.Dropped)
        {
            
            Bounds myBounds = Utility.GetBounds2D(transform);
            Bounds otherBounds = Utility.GetBounds2D(pc.transform);
            //float toMove = myBounds.size
            CancelInvoke("ReenableCollision");
            //collider.enabled = false;

            //body.AddForce(new Vector2(pc.transform.forward.x * 300, 300), ForceMode2D.Force);
            //FinalizeDropped();
            Invoke("FinalizeDropped", 0.5f);
        }
        else if(dropType == DropType.BrokeFree)
        {
            FinalizeDropped();
        }
        OnDropped?.Invoke(this, dropType);
        return true;
    }

    void FinalizeDropped()
    {
        CancelInvoke("FinalizeDropped");
        pickedUpBy = null;
        coll.enabled = true;
        GameObject.Destroy(joint);
        joint = null;
    }
}
