using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePortals_Activation : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject oldPortal;
    [SerializeField] private GameObject newPortal;

    public void Activate()
    {
        oldPortal.SetActive(false);
        newPortal.SetActive(true);
    }

    public void Deactivate()
    {
        newPortal.SetActive(false);
        oldPortal.SetActive(true);
    }
}
