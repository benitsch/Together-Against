using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnInteractableActiveChanged(Interactable me, bool isActive);

public class Interactable : MonoBehaviour
{
    public OnInteractableActiveChanged OnActivationStateChanged;

    [SerializeField]
    private List<Activateable> Activateables = new List<Activateable>();
    private void Awake()
    {
        foreach (Activateable a in Activateables)
        {
            a.LinkUp(this);
        }
    }

    public void SetActiveState(bool isActive)
    {
        OnActivationStateChanged?.Invoke(this, isActive);
    }
}
