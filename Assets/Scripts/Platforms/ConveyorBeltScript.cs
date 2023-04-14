using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterMovement movement = collision.GetComponent<CharacterMovement>();
        if(movement)
        {
            movement.LockMovement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CharacterMovement movement = collision.GetComponent<CharacterMovement>();
        if (movement)
        {
            movement.UnlockMovement();
        }
    }
}
