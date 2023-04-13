using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Start is called before the first frame update
    public void Damage()
    {
        Damage_Implementation();
    }

    public virtual void Damage_Implementation()
    {

    }
}
