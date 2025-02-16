using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperties : MonoBehaviour
{
    [SerializeField] private bool _isDestroyable;
    [SerializeField] private bool _isPickupable;
    [SerializeField] private bool _isObjectInPast;
    [SerializeField] private float _objectWeight;

    public bool isDestroyable()
    {
        return _isDestroyable; 
    }

    public bool isPickupable()
    {
        return _isPickupable;
    }

    public bool isObjectInPast()
    {
        return _isObjectInPast;
    }

    public float weight()
    {
        return _objectWeight;
    }
}
