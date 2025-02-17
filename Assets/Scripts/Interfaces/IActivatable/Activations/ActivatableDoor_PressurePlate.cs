using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableDoor_PressurePlate : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject doorObject;
 
    public void Activate()
    {
        doorObject.SetActive(false);
    }
    public void Deactivate()
    {
        doorObject.SetActive(true);
    }

}
