using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLight_Pickup : MonoBehaviour, IInteraction
{
    
    [HideInInspector] public bool messageDisplayed = false;
    public virtual void Interact()
    {
        //BlackLightController.instance.HasLight(true);
        ToggleFPLightCam.instance._hasTorch = true;
        Destroy(this.gameObject);
    }
}