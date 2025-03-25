using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalEmitter_PressurePlate : MonoBehaviour, IActivatable
{
    [HideInInspector] public bool signal; 
    public void Activate()
    {
        signal = true;
    }
    
    public void Deactivate()
    {
        signal = false;
    }
}
