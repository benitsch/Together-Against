using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivationRequirementMode
{
    OnlyOne,
    All
}
public delegate void OnIsActivatedChangedDelegate(Activateable a, bool isActive);
public class Activateable : MonoBehaviour
{
    private List<Interactable> LinkedInteractables = new List<Interactable>();
    OnIsActivatedChangedDelegate OnIsActivatedChanged;
    public ActivationRequirementMode ActivationMode;

    private int Counter = 0;
    public bool IsActivated { private set; get;} = false;
    public bool CanEverBeActivated { private set; get; } = false;
    public void Activate()
    {
        IsActivated = true;
        Activate_Implementation();
        OnIsActivatedChanged?.Invoke(this, IsActivated);
    }

    public void Deactivate()
    {
        IsActivated = false;
        Deactivate_Implementation();
        OnIsActivatedChanged?.Invoke(this, IsActivated);
    }

    protected virtual void Activate_Implementation()
    {
        
    }

    protected virtual void Deactivate_Implementation()
    {

    }

    public void LinkUp(Interactable i)
    {
        LinkedInteractables.Add(i);
        i.OnActivationStateChanged += NotifyInteractableActiveChanged;
        CanEverBeActivated = true;
    }

    private void NotifyInteractableActiveChanged(Interactable i, bool isActive)
    {
        if (CanEverBeActivated)
        {
            Debug.Assert(LinkedInteractables.Contains(i));
            if (isActive)
            {
                this.Counter++;
                if (ActivationMode == ActivationRequirementMode.OnlyOne && this.Counter == 1)
                {
                    this.Activate();
                }
                else if (ActivationMode == ActivationRequirementMode.All && this.Counter == LinkedInteractables.Count)
                {
                    this.Activate();
                }
            }
            else if (!isActive)
            {
                Debug.Assert(LinkedInteractables.Contains(i));
                this.Counter--;
                if (ActivationMode == ActivationRequirementMode.OnlyOne && this.Counter == 0)
                {
                    this.Deactivate();
                }
                else if (ActivationMode == ActivationRequirementMode.All && this.Counter < LinkedInteractables.Count)
                {
                    this.Deactivate();
                }
            }
        }
    }
}
