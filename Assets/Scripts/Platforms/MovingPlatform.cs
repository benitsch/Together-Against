using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Activateable
{
    [SerializeField] private Transform posA, posB;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private bool defaultIsMoving = true;
    
    private Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = posB.position;
        
        if (defaultIsMoving) {
            Activate();
        }
    }

    // Update is called once per frame
    void Update()
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

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(posA.position, posB.position);
    }
}
