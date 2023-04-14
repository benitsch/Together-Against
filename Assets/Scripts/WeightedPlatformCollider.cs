using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlatformCollider : MonoBehaviour
{
    [SerializeField] private LayerMask layers;
    public bool InContact { get; private set; }
    public float Mass { get; private set; }
    private Vector3 Origin;

    private void Start()
    {
        Origin = transform.position;
    }

    public void ReturnToOrigin(float speed)
    {
        if (transform.position.y != Origin.y)
        {
            Vector3 velocity = Origin - transform.position;
            if (velocity.magnitude > 1) velocity = velocity.normalized;
            GetComponent<Rigidbody2D>().velocity = velocity * speed;
        }
    }

    public void SetVelocity(Vector3 velocity, float maxDistance)
    {
        float distance = (Origin.y - transform.position.y) * -1;
        if (velocity.y < 0)
        {
            if (distance > 0) distance = 0;
            distance = -maxDistance - distance;
            if (distance > 0) velocity *= -1;
        }
        else if (velocity.y > 0)
        {
            if (distance < 0) distance = 0;
            distance = maxDistance - distance;
            if (distance < 0) velocity *= -1;
        }

        distance = Mathf.Clamp(Mathf.Abs(distance), 0, 1);
        GetComponent<Rigidbody2D>().velocity = velocity * distance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Utility.LayerMaskHas(layers, collision.gameObject.layer)) return;

        if (Mass < 0) Mass = 0;
        Mass += collision.gameObject.GetComponent<Rigidbody2D>().mass;
        InContact = Mass > 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!Utility.LayerMaskHas(layers, collision.gameObject.layer)) return;

        Mass -= collision.gameObject.GetComponent<Rigidbody2D>().mass;
        if (Mass < 0) Mass = 0;
        InContact = Mass > 0;
    }
}
