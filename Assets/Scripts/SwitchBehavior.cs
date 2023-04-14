using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : Interactable
{
    // public Sprite spriteStateOff;
    // public Sprite spriteStateOn;
    private SpriteRenderer spriteRenderer;
    private bool state = false; // off (false) or on (true)
    private bool isAcitvateable = false;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if (!isPlayer(other)) {
    //         return;
    //     }

    //     if (InputTrigger()) {
    //         state = !state;
    //         SetActiveState(state);
    //         Debug.Log("State changed to " + state);
    //     }
    // }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayer(other)) {
            isAcitvateable = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (isPlayer(other)) {
            isAcitvateable = false;
        }
    }

    public override void Interact(PlayerController pc)
    {
        state = !state;
        SetActiveState(state);
        Debug.Log("State changed to " + state);
    }

    // bool InputTrigger()
    // {
    //     return Input.GetKeyDown(KeyCode.E);
    // }

    private bool isPlayer(Collider2D other)
    {
        return other.gameObject.tag == "Player";
    }
}
