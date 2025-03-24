using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableDoor_PressurePlate : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject doorObject, doorObjAS;
 
    public void Activate()
    {
        doorObject.SetActive(false);
  //      doorObjAS.GetComponent<AudioSource>().Play();
    }
    public void Deactivate()
    {
        doorObject.SetActive(true);
  //      doorObjAS.GetComponent<AudioSource>().Stop();

    }

}
