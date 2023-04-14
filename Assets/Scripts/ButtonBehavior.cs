using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : Interactable
{
    //TODO sprites for pressed and released & sound when art is ready

    //public Sprite onSprite; // The Sprite for button pressed
    //public Sprite offSprite;
    //public AudioClip pressSound; // The sound when the button is pressed

    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isPressed = false;

    [SerializeField] private int pressedCounter = 0; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pressedCounter++;
            if(pressedCounter == 1)
            {
                SetActiveState(true);
            }
            //spriteRenderer.sprite = isPressed ? onSprite : offSprite;
            //AudioSource.PlayClipAtPoint(pressSound, transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pressedCounter--;
            if(pressedCounter == 0)
            {
                SetActiveState(false);
            }
            //spriteRenderer.sprite = isPressed ? onSprite : offSprite;
            //AudioSource.PlayClipAtPoint(pressSound, transform.position);
        }
    }
}
