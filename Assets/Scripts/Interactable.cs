using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnInteractableActiveChanged(Interactable me, bool isActive);

public class Interactable : MonoBehaviour
{
    public OnInteractableActiveChanged OnActivationStateChanged;

    [SerializeField]
    private List<Activateable> Activateables = new List<Activateable>();
    protected virtual void Awake()
    {
        Activateables.RemoveAll( a => a == null );
        foreach (Activateable a in Activateables)
        {
            a.LinkUp(this);
        }
    }

    public virtual void Interact(PlayerController pc)
    {
    }

    public void SetActiveState(bool isActive)
    {
        OnActivationStateChanged?.Invoke(this, isActive);
    }
}
