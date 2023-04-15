using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedActivator : Interactable
{
    [Range(0.5f, 30f)]
    public float ActiveDuration = 5.0f;

    [Range(0.5f, 30f)]
    public float InactiveDuration = 5.0f;

    public bool startActive = false;
    [ReadOnly] public bool isActive = false;
    private void Awake()
    {
        base.Awake();
        isActive = startActive;
    }
    // Start is called before the first frame update
    void Start()
    {
        //start immediately
        Invoke("TriggerTimer", isActive ? ActiveDuration : InactiveDuration);
    }

    void TriggerTimer()
    {
        isActive = !isActive;
        SetActiveState(isActive);
        Invoke("TriggerTimer", isActive ? ActiveDuration : InactiveDuration);
    }
}
