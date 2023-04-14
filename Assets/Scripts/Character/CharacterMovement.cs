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

    public bool canEverJump = true;
    private bool wantsToJump = false;

    private void Awake()
    {
        body = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if(body == null || spriteRenderer == null)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = movementInput.x < 0;
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

        Vector2 velocity = body.velocity;
        Vector2 oldVeocity = velocity;
        if (movementInput.x != 0)
        {
            velocity = new Vector2(movementInput.x * movementSpeed * Time.fixedDeltaTime, body.velocity.y);
        }
        
        if (wantsToJump)
        {
            if (IsGrounded())
            {
                velocity += new Vector2(0, jumpForce);
            }
            //rb.AddForce(new Vector2(0, jumpForce * 100));
            wantsToJump = false;
        }

        body.velocity = Vector3.SmoothDamp(body.velocity, velocity, ref currentVelocity, movementSmoothing);

        if(oldVeocity.x != velocity.x)
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    public bool IsGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);

        return colliders != null;
    }
}
