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

    [ReadOnly] public bool movementControlsLocked = false;

    public OnUseKeyPressedDelegate OnUseKeyPressed;

    [ReadOnly, SerializeField] private Pickup pickedUpItem = null;
    public Pickup MyPickup;
    public Transform pickupSlotLocation;
    public Transform pickupScanLocation;
    [Range(0.1f, 1.0f)] public float pickupScanRadius;

    public LayerMask pickupsLayerMask;
    public LayerMask interactableLayerMask;

    CharacterMovement movement;

    private void Awake()
    {
        Pickup myPickup = GetComponent<Pickup>();
        if(MyPickup)
        {
            myPickup.OnPickedUp += NotifyPickedUp;
            myPickup.OnDropped += NotifySelfDropped;
        }
        movement = GetComponent<CharacterMovement>();
        if(movement == null)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void NotifyPickedUp(Pickup pickedUpItem)
    {
        movementControlsLocked = true;
    }

    private void NotifySelfDropped(Pickup pickup, DropType dropType)
    {
        movementControlsLocked = false;
    }

    private void Update()
    {
        float horizontal = movementControlsLocked ? 0 : (Input.GetKey(moveRightKey) ? 1 : 0) + (Input.GetKey(moveLeftKey) ? -1 : 0);

        movement.AddMovementInput(new Vector2(horizontal, 0));

        if (Input.GetKeyDown(upKey))
        {
            movement.Jump();
        }

        if(Input.GetKeyDown(useKey))
        {
            OnUseKeyPressed?.Invoke();
            if(!TryPickup())
            {
                TryInteract();
            }
        }
    }

    bool TryPickup()
    {
        if(pickedUpItem)
        {
            if(pickedUpItem.Drop(this))
            {
                pickedUpItem = null;
            }
            return true;
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
