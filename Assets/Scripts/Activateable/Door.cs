using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    Idle,
    Moving
}
public class Door : Activateable
{
    public Transform secondTransform;
    [ReadOnly] public Vector2 secondLocation;
    [ReadOnly] public Vector2 originalLocation;
    [ReadOnly] public Vector3 targetLocation;

    Rigidbody2D body;

    [Range(0.1f, 100f)]
    public float MoveSpeed = 5;

    [ReadOnly] public DoorState doorState = DoorState.Idle;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    protected void Start()
    {
        originalLocation = transform.position;
        secondLocation = secondTransform.position;
    }

    protected override void Activate_Implementation()
    {
        targetLocation = secondLocation;
        doorState = DoorState.Moving;
    }

    protected override void Deactivate_Implementation()
    {
        targetLocation = originalLocation;
        doorState = DoorState.Moving;
    }

    private void FixedUpdate()
    {
        if (doorState == DoorState.Moving)
        {
            Vector3 dir = (targetLocation - transform.position).normalized;

            body.MovePosition(transform.position + dir * (MoveSpeed * Time.fixedDeltaTime));
            
            if (Vector3.Distance(targetLocation, transform.position) < 0.1)
            {
                transform.position = targetLocation;
                doorState = DoorState.Idle;
            }
        }
    }
}
