using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void FixedUpdate()
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
            PlatformRight.GetComponent<Rigidbody2D>().velocity = PlatformLeft.GetComponent<Rigidbody2D>().velocity * -1;
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
