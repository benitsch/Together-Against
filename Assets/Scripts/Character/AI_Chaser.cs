using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Chaser : MonoBehaviour
{
    public Transform target;
    public float targetCheckInterval = 0.5f;
    public float targetCheckTimer = 0.0f;
    public float SightRange = 5.0f;
    public LayerMask targetLayerMask;
    public LayerMask terrainLayerMask;

    CharacterMovement movement;

    void Awake()
    {
        movement = GetComponentInChildren<CharacterMovement>();    
        if(!movement)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetCheckTimer += Time.deltaTime;
        if (targetCheckTimer > targetCheckInterval)
        {
            target = null;
            targetCheckTimer = 0.0f;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, SightRange, targetLayerMask);
            foreach (var coll in colliders)
            {
                target = coll.transform;
                break;
            }
        }

        Vector2 movementInput = Vector2.zero;

        if (target)
        {
            float distanceToPlayer = target.position.x - transform.position.x;
            if (Mathf.Abs(distanceToPlayer) > 1)
            {
                movementInput.x = Mathf.Clamp(distanceToPlayer, -1.0f, 1.0f);
                movement.AddMovementInput(movementInput);
            }
        }

        movement.AddMovementInput(movementInput);

        if (movementInput.x != 0 && movement.IsGrounded() && Physics2D.Raycast(transform.position, Vector2.right * movementInput.x, 1.0f, terrainLayerMask))
        {
            movement.Jump();
        }
    }
}
