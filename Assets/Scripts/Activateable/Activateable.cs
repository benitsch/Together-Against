using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void OnActivationChangedDelegate(bool isActive);
public class Activateable : MonoBehaviour
{
    OnActivationChangedDelegate OnActiveChanged;
    [SerializeField]
    private int Counter = 0;
    // Start is called before the first frame update
    public void Activate()
    {
        this.Counter++;
        if(this.Counter == 1)
        {
            OnActiveChanged?.Invoke(true);
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
            OnActiveChanged?.Invoke(false);
            this.Deactivate_Implementation();
        }
    }

    protected virtual void Deactivate_Implementation()
    {

    }

    public bool IsActive()
    {
        return this.Counter > 0;
    }
}
