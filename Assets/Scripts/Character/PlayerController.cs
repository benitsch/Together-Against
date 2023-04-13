using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        if(movement == null)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        movement.input = new Vector2(horizontal, 0);

        if (Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }
    }
}
