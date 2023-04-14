using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Activateable
{
    public bool StartActive = false;

    public LayerMask hittableLayers;
    
    void Awake()
    {
        if(!StartActive)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void Activate_Implementation()
    {
        gameObject.SetActive(!StartActive);
    }

    protected override void Deactivate_Implementation()
    {
        gameObject.SetActive(StartActive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable dmg = collision.gameObject.GetComponent<Damageable>();
        if(dmg != null)
        {
            dmg.Damage();
        }
    }
}
