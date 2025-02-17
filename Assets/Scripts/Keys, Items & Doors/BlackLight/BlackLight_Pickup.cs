using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLight_Pickup : MonoBehaviour, IInteraction
{
    public virtual void Interact()
    {
        BlackLightController.instance.HasLight(true);

        Destroy(this.gameObject);
    }
}