using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivationRequirementMode
{
    OnlyOne,
    MinumumRequired,
    Toggle,
    All
}
public delegate void OnIsActivatedChangedDelegate(Activateable a, bool isActive);
public class Activateable : MonoBehaviour
{
    public List<Interactable> LinkedInteractables = new List<Interactable>();
    public OnIsActivatedChangedDelegate OnIsActivatedChanged;
    public ActivationRequirementMode ActivationMode;
    [Range(1,10)]
    public int MinimumRequired = 1;

    public int Counter = 0;
    [SerializeField]
    public bool IsActivated = false;
    [SerializeField]
    public bool CanEverBeActivated = false;
    public void Activate()
    {
        if(IsActivated)
        {
            return;
        }
        IsActivated = true;
        Activate_Implementation();
        OnIsActivatedChanged?.Invoke(this, IsActivated);
    }

    public void Deactivate()
    {
        if(!IsActivated)
        {
            return;
        }
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
            if (ActivationMode == ActivationRequirementMode.Toggle)
            {
                if (IsActivated)
                {
                    Deactivate();
                }
                else
                {
                    Activate();
                }
            }
            else
            {
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
                    else if (ActivationMode == ActivationRequirementMode.MinumumRequired && this.Counter >= MinimumRequired)
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
                    else if (ActivationMode == ActivationRequirementMode.MinumumRequired && this.Counter < MinimumRequired)
                    {
                        this.Deactivate();
                    }
                }
            }
        }
    }
}
