using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInterface : MonoBehaviour, IInteraction
{
    [SerializeField] private Camera oldCamera;
    [SerializeField] private Camera newCamera;
    [Tooltip("This will be how far the player moves on the x axis")] 
    [SerializeField] private float travelLocation = 1000;
    
    public virtual void Interact()
    {
        TimeTravelController.Instance.TimeTravel(oldCamera, newCamera, travelLocation);
    }
}
