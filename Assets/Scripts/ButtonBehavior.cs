using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : Interactable
{
    //TODO sprites for pressed and released & sound when art is ready

    [SerializeField] private Sprite onSprite; // The Sprite for button pressed
    [SerializeField] private Sprite offSprite;
    [SerializeField] private AudioClip pressSound; // The sound when the button is pressed
    [SerializeField] private bool staysPressed = false; // If the button stays pressed for ever

    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;

    private int pressedCounter = 0; 

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isPressed)
        {
            pressedCounter++;
            if (pressedCounter == 1)
            {
                isPressed = true;
                AudioSource.PlayClipAtPoint(pressSound, transform.position);
                SetActiveState(true);
                spriteRenderer.sprite = onSprite;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isPressed && !staysPressed)
        {
            pressedCounter--;
            if (pressedCounter == 0)
            {
                isPressed = false;
                AudioSource.PlayClipAtPoint(pressSound, transform.position);
                SetActiveState(false);
                spriteRenderer.sprite = offSprite;
            }
        }
    }
}
