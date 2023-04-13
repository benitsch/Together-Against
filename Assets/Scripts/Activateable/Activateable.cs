using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activateable : MonoBehaviour
{
    [SerializeField]
    public int Counter = 0;
    // Start is called before the first frame update
    public void Activate()
    {
        this.Counter++;
        if(this.Counter == 1)
        {
            this.Activate_Implementation();
        }
    }

    protected virtual void Activate_Implementation()
    {

    }

    public void Deactivate()
    {
        this.Counter--;
        Debug.Assert(this.Counter >= 0);
        if(this.Counter == 0)
        {
            this.Dectivate_Implementation();
        }
    }

    protected virtual void Dectivate_Implementation()
    {

    }
}
