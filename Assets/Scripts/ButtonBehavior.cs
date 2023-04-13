using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    //TODO sprites for pressed and released & sound when art is ready

    //public Sprite onSprite; // The Sprite for button pressed
    //public Sprite offSprite;
    //public AudioClip pressSound; // The sound when the button is pressed

    [SerializeField] private Activateable activateable;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isPressed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPressed = !isPressed;
            if (activateable != null)
            {
                if (isPressed)
                {
                    activateable.Activate();
                } else
                {
                    activateable.Deactivate();
                }
            }
            //spriteRenderer.sprite = isPressed ? onSprite : offSprite;
            Debug.Log("Pressed!");
            //AudioSource.PlayClipAtPoint(pressSound, transform.position);
        }
    }
}
