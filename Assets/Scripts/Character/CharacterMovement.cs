using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Range(100, 500)] private float movementSpeed = 200f;
    [SerializeField] [Range(1, 20)] private float jumpForce = 2f;
    [SerializeField] [Range(0, 1)] private float movementSmoothing = 0f;
    Vector3 currentVelocity = Vector3.zero;

    [Header("GroundedSettings")]
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] [Range(0.001f, 1f)] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;

    private Vector2 movementInput =  Vector2.zero;
    private Vector2 previousMovementInput = Vector2.zero;
    
    public bool canEverJump = true;
    private bool wantsToJump = false;

    [ReadOnly, SerializeField] bool bIsOnGround = false;

    public Animator animator;
    [ReadOnly, SerializeField] private Pickup myPickup;
    [ReadOnly, SerializeField] private bool isPickedUp = false;

    [ReadOnly, SerializeField] protected int lockMovementCounter = 0;
    [ReadOnly, SerializeField] protected float lockMovementTimer = 0;

    

    private void Awake()
    {
        myPickup = GetComponent<Pickup>();
        if(myPickup)
        {
            myPickup.OnPickedUp += NotifyPickedUp;
            myPickup.OnNoLongerPickedUp += NotifyNoLongerPickedUp;
        }
        animator = GetComponentInChildren<Animator>();
        body = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void NotifyNoLongerPickedUp(Pickup pickedUpItem, DropType dropType)
    {
        isPickedUp = false;
        if(dropType == DropType.Thrown)
        {
            lockMovementTimer = 0.5f;
        }
    }

    private void NotifyPickedUp(Pickup pickedUpItem)
    {
        isPickedUp = true;
    }

    private void Start()
    {
        if(body == null || spriteRenderer == null)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void LockMovement()
    {
        lockMovementCounter++;
    }

    public void UnlockMovement()
    {
        lockMovementCounter--;
    }

    public bool IsMovementLocked()
    {
        return isPickedUp || lockMovementCounter > 0 || lockMovementTimer > 0;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", Math.Abs(body.velocity.x));
    }

    public void Jump()
    {
        if(canEverJump)
        {
            wantsToJump = true;
        }
    }

    public void AddMovementInput(Vector2 input)
    {
        movementInput = input;
    }

    private void FixedUpdate()
    {
        bool skipMovementCalc = IsMovementLocked();
        if (skipMovementCalc)
        {
            movementInput = Vector2.zero;
        }
        lockMovementTimer = Math.Max(0, lockMovementTimer - Time.fixedDeltaTime);

        Vector2 velocity = body.velocity;

        bool isGrounded = IsGrounded();

        animator.SetBool("inAir", !isGrounded);

        if (movementInput.x != 0 && movementInput.x != previousMovementInput.x)
        {
            transform.rotation = movementInput.x > 0 ? Quaternion.Euler(0, 0f, 0) : Quaternion.Euler(0, 180f, 0);
        }
        if(!skipMovementCalc)
        {
            if (movementInput.x != 0)
            {
                velocity = new Vector2(movementInput.x * movementSpeed * Time.fixedDeltaTime, velocity.y);
            }
            else if (!isGrounded)
            {
                velocity.x = 0;
            }
        }
        if (wantsToJump)
        {
            if (IsGrounded())
            {
                if (isPickedUp)
                {
                    myPickup.BreakFree();
                }
                velocity += new Vector2(0, jumpForce);
            }
        }
        wantsToJump = false;

        body.velocity = skipMovementCalc ? body.velocity : Vector3.SmoothDamp(body.velocity, velocity, ref currentVelocity, movementSmoothing);

        previousMovementInput = movementInput;
    }

    private void LateUpdate()
    {
        Debug.Log(body.velocity);
    }

    public bool IsGrounded()
    {
        int oldLayer = gameObject.layer; // This variable now stored our original layer
        gameObject.layer = 2; // The game object will now ignore all forms of raycasting
        Collider2D colliders = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);
        gameObject.layer = oldLayer;

        return colliders != null;
    }
}
