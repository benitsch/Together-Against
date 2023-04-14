using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Activateable))]
public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private bool FallOnExit = false;
    [SerializeField] private float EnterTime = 6;
    [SerializeField] private float ExitTime = 3;
    [SerializeField] private float FallTime = 5;
    [SerializeField] private float RespawnTime = 10;
    [SerializeField, Layer] private int TriggerLayer = 0;
    [SerializeField] private float mass = 10;
    [SerializeField] private float drag = 10;

    private bool triggerd = false;
    private Vector3 position;
    Activateable link;

    private void Awake()
    {
        position = transform.position;
        GetComponent<Rigidbody2D>().mass = mass;
        GetComponent<Rigidbody2D>().drag = drag;
        link = GetComponent<Activateable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((FallOnExit || triggerd) ||
            (link.CanEverBeActivated && !link.IsActivated) ||
            (collision.collider.gameObject.layer != TriggerLayer))
        {
            return;
        }

        triggerd = true;
        Invoke("StartFalling", EnterTime);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((!FallOnExit || triggerd) ||
            (link.CanEverBeActivated && !link.IsActivated) ||
            (collision.collider.gameObject.layer != TriggerLayer))
        {
            return;
        }

        triggerd = true;
        Invoke("StartFalling", ExitTime);
    }

    void StartFalling()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.bodyType == RigidbodyType2D.Kinematic) rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("GoInactive", FallTime);
    }

    void GoInactive()
    {
        Invoke("Respawn", RespawnTime);
        gameObject.SetActive(false);
    }

    void Respawn()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        transform.position = position;
        triggerd = false;
        gameObject.SetActive(true);
    }
}
