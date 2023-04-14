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

    public OnUseKeyPressedDelegate OnUseKeyPressed;

    Pickup pickedUp = null;

    public Transform pickupSlotLocation;
    public float pickupScanRadius;

    public LayerMask pickupsLayerMask;
    public LayerMask interactableLayerMask;

    CharacterMovement movement;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        if(movement == null)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        float horizontal = (Input.GetKey(moveRightKey) ? 1 : 0) + (Input.GetKey(moveLeftKey) ? -1 : 0);

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
        /*
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(pickupSlotLocation.position, pickupScanRadius, pickupsLayerMask);
        foreach(Collider2D coll in overlaps)
        {
            if(coll.gameObject == gameObject)
            {
                continue;
            }
        }
        */
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
