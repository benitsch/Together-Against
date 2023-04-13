using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode useKey = KeyCode.E;
    public KeyCode downKey = KeyCode.S;
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
        float horizontal = Input.GetAxisRaw("Horizontal");

        movement.AddMovementInput(new Vector2(horizontal, 0));

        if (Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }
    }
}
