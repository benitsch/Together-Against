using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    //public Sprite highlightedSprite; // The Sprite for button pressed
    //public AudioClip pressSound; // The sound when the button is pressed

    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isPressed)
        {
            isPressed = true;
            Debug.Log("Pressed!");
            //spriteRenderer.sprite = highlightedSprite;
            //AudioSource.PlayClipAtPoint(pressSound, transform.position);
        }
    }
}
