using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformDirection
{
    PositionA,
    PositionB
}

public class MovingPlatform : Activateable
{
    [SerializeField] private Transform posA, posB;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private bool defaultIsMoving = true;
    [SerializeField] private bool beginAnywhere = false;
    [SerializeField] private PlatformDirection beginDirection = PlatformDirection.PositionB;
    
    private Vector2 targetPos;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (beginDirection == PlatformDirection.PositionA) {
            targetPos = posA.position;
        } else {
            targetPos = posB.position;
        }
        
        if (defaultIsMoving) {
            Activate();
        }

        if (!beginAnywhere) {
            transform.position = (posA.position + posB.position) /2f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsActivated) {
            return;
        }

        if (Vector2.Distance(transform.position, posA.position) < 0.1f)
        {
            targetPos = posB.position;
        }
        if (Vector2.Distance(transform.position, posB.position) < 0.1f)
        {
            targetPos = posA.position;
        }

        body.MovePosition(Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(posA.position, posB.position);
    }
}
