using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnUseKeyPressedDelegate();
public class PlayerController : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode useKey = KeyCode.E;
    public KeyCode throwKey = KeyCode.F;

    [Range(0, 1)] public int playerID = 0;

    [ReadOnly] public int movementControlsLockCounter = 0;

    public OnUseKeyPressedDelegate OnUseKeyPressed;

    [ReadOnly, SerializeField] private Pickup pickedUpItem = null;
    public Transform pickupSlotLocation;
    public Transform pickupScanLocation;
    [Range(0.1f, 1.0f)] public float pickupScanRadius;

    [Range(1f, 20f)]
    public float ThrowVelocity = 20f;

    public LayerMask pickupsLayerMask;
    public LayerMask interactableLayerMask;

    CharacterMovement movement;

    Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        Pickup myPickup = GetComponent<Pickup>();
        if(myPickup)
        {
            myPickup.OnPickedUp += NotifyPickedUp;
            myPickup.OnNoLongerPickedUp += NotifyNoLongerPickedUp;
        }
        movement = GetComponent<CharacterMovement>();
        if(movement == null)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void LockMoveControls()
    {
        movementControlsLockCounter++;
    }

    public void UnlockMoveControls()
    {
        movementControlsLockCounter--;
    }
    private void NotifyNoLongerPickedUp(Pickup pickedUpItem, DropType dropType)
    {
        movementControlsLockCounter++;
    }

    private void NotifyPickedUp(Pickup pickedUpItem)
    {
        movementControlsLockCounter--;
    }

    private void Update()
    {
        float horizontal = movementControlsLockCounter > 0 ? 0 : (Input.GetKey(moveRightKey) ? 1 : 0) + (Input.GetKey(moveLeftKey) ? -1 : 0);
        movement.AddMovementInput(new Vector2(horizontal, 0));

        if (Input.GetKeyDown(upKey))
        {
            movement.Jump();
        }

        if(Input.GetKeyDown(useKey))
        {
            OnUseKeyPressed?.Invoke();
            if(!DropHeldItem())
            {
                if (!TryPickup())
                {
                    TryInteract();
                }
            }
        }

        if(Input.GetKeyDown(throwKey))
        {
            ThrowHeldObject();
        }
    }

    void ThrowHeldObject()
    {
        if(!pickedUpItem)
        {
            return;
        }

        Vector2 throwVelocity = new Vector2(transform.right.x * ThrowVelocity + myBody.velocity.x, ThrowVelocity + myBody.velocity.y);
        pickedUpItem.Throw(throwVelocity);
        pickedUpItem = null;
        return;
    }
    bool DropHeldItem()
    {
        if(!pickedUpItem)
        {
            return false;
        }

        Vector2 dropLocation = new Vector2(pickupScanLocation.position.x + transform.right.x * pickedUpItem.coll.bounds.extents.x, pickupScanLocation.position.y + pickedUpItem.coll.bounds.extents.y);

        pickedUpItem.Drop(this, dropLocation);
        pickedUpItem = null;

        return true;
    }

    bool TryPickup()
    {
        if(pickedUpItem)
        {
            return false;
        }
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(pickupScanLocation.position, pickupScanRadius, pickupsLayerMask);
        foreach(Collider2D coll in overlaps)
        {
            if(coll.gameObject == gameObject)
            {
                continue;
            }
            Pickup p = coll.GetComponent<Pickup>();
            if(p)
            {
                if(p.TryPickup(this))
                {
                    pickedUpItem = p;
                    p.PickedUp(this);
                    return true;
                }
            }
        }
        return false;
    }

    void TryInteract()
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, pickupScanRadius, interactableLayerMask);
        foreach (Collider2D coll in overlaps)
        {
            if (coll.gameObject == gameObject)
            {
                continue;
            }
            Interactable i = coll.GetComponent<Interactable>();
            if (i != null)
            {
                i.Interact(this);
            }
        }
    }
}
