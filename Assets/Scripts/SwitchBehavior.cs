using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : Interactable
{
    private SpriteRenderer spriteRenderer;
    public Sprite spriteStateOff;
    public Sprite spriteStateOn;
    public AudioClip audioTrigger;
    private bool state = false; // off (false) or on (true)
    private bool isActivateable = false;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSprite();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        SetIsActivateable(collider, true);
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        SetIsActivateable(collider, false);
    }

    public override void Interact(PlayerController pc)
    {
        state = !state;
        SetActiveState(state);
        ChangeSprite();
        AudioSource.PlayClipAtPoint(audioTrigger, transform.position);
        // Debug.Log("State changed to " + state);
    }

    private void SetIsActivateable(Collider2D collider, bool activateable)
    {
        if (IsPlayer(collider)) {
            isActivateable = activateable;
        }
    }

    private bool IsPlayer(Collider2D other)
    {
        return other.gameObject.tag == "Player";
    }

    private void ChangeSprite()
    {
        spriteRenderer.sprite = state ? spriteStateOn : spriteStateOff;
    }
}
