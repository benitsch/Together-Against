using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : Interactable
{
    //TODO sprites for pressed and released & sound when art is ready

    [SerializeField] private Sprite onSprite; // The Sprite for button pressed
    [SerializeField] private Sprite offSprite;
    [SerializeField] private AudioClip pressSound; // The sound when the button is pressed

    private SpriteRenderer spriteRenderer;
    //[SerializeField] private bool isPressed = false;

    private int pressedCounter = 0; 

    void Awake()
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
                AudioSource.PlayClipAtPoint(pressSound, transform.position);
                SetActiveState(true);
                spriteRenderer.sprite = onSprite;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pressedCounter--;
            if(pressedCounter == 0)
            {
                AudioSource.PlayClipAtPoint(pressSound, transform.position);
                SetActiveState(false);
                spriteRenderer.sprite = offSprite;
            }
        }
    }
}
