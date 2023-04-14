using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Activateable))]
public class WeightedPlatform : MonoBehaviour
{
    [SerializeField] private float MoveDistance;
    [SerializeField] private Color DrawColor;
    [SerializeField] private float AdjustSpeed;
    [SerializeField] private float BalanceSpeed;
    [SerializeField] private float FloatTimer;

    [SerializeField] private WeightedPlatformCollider PlatformLeft;
    [SerializeField] private WeightedPlatformCollider PlatformRight;

    private float Timer = 0;
    Activateable link;

    private void Start()
    {
        link = GetComponent<Activateable>();
    }

    private void FixedUpdate()
    {
        if (link.CanEverBeActivated && !link.IsActivated)
        {
            PlatformLeft.SetVelocity(Vector2.zero, MoveDistance);
            PlatformRight.SetVelocity(Vector2.zero, MoveDistance);
        }
        else
        {
            if (PlatformLeft.InContact || PlatformRight.InContact) Timer = FloatTimer;

            if (Timer > 0)
            {
                Timer -= Time.deltaTime;

                float speed = PlatformLeft.Mass - PlatformRight.Mass;
                if (speed > 0) speed = AdjustSpeed;
                else if (speed < 0) speed = -AdjustSpeed;
                Vector3 velocity = Vector2.down * speed;
                PlatformLeft.SetVelocity(velocity, MoveDistance);
                PlatformRight.SetVelocity(velocity * -1, MoveDistance);
            }
            else if (BalanceSpeed > 0 && !PlatformLeft.InContact && !PlatformRight.InContact)
            {
                PlatformLeft.ReturnToOrigin(BalanceSpeed);
                PlatformRight.ReturnToOrigin(BalanceSpeed);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 Origin, Target;
        Gizmos.color = DrawColor;

        Origin = PlatformLeft.transform.position + Vector3.down * MoveDistance;
        Target = PlatformLeft.transform.position + Vector3.up * MoveDistance;
        Gizmos.DrawLine(Origin, Target);

        Origin = PlatformRight.transform.position + Vector3.down * MoveDistance;
        Target = PlatformRight.transform.position + Vector3.up * MoveDistance;
        Gizmos.DrawLine(Origin, Target);
    }
}
