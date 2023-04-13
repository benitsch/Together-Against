using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    //TODO sprites for pressed and released & sound when art is ready

    //public Sprite onSprite; // The Sprite for button pressed
    //public Sprite offSprite;
    //public AudioClip pressSound; // The sound when the button is pressed

    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPressed = !isPressed;
            //spriteRenderer.sprite = isPressed ? onSprite : offSprite;
            Debug.Log("Pressed!");
            //AudioSource.PlayClipAtPoint(pressSound, transform.position);
        }
    }
}
