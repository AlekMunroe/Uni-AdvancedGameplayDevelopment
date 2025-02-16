using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight_PressurePlate : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject light;

    public void Activate()
    {
        light.SetActive(true);
    }

    public void Deactivate()
    {
        light.SetActive(false);
    }
}
