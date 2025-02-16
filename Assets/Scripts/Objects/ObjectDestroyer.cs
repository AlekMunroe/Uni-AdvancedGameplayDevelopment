using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        ObjectProperties properties = other.gameObject.GetComponent<ObjectProperties>();
        PastPresentController pastPresentController = other.gameObject.GetComponent<PastPresentController>();
        
        //Check if the object can be destroyed
        if (properties != null && properties.isDestroyable())
        {
            if (pastPresentController != null)
            {
                //Destroy if the past present controller exists
                pastPresentController.DestroyThis();
            }
            else
            {
                //Destroy without the past present controller
                Destroy(other.gameObject);
            }
        }
    }
}
