using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContainerController : MonoBehaviour, IInteraction
{
    [Header("Container Settings")]
    [SerializeField] private string contName;
    [SerializeField] private string requiredKey;
    [SerializeField] private bool needsKey;
    
    [Header("Container References")]
    [SerializeField] private GameObject containerObject;
    [SerializeField] private GameObject contents;

    public void Start()
    {
        if (!needsKey)
        {
            requiredKey = null;
        }
    }

    public virtual void Interact()
    {
        switch (requiredKey)
        {
            case null when !needsKey:
            {
                //No key required for this container
                containerObject.SetActive(false);
                if (!contents.activeSelf)
                {
                    contents.SetActive(true);
                }

                break;
            }
            case null when needsKey:
                Debug.LogError("Error: No key was set for the container: " + contName + ". Please set a key or disable the needsKey option.");
                break;
            default:
            {
                bool hasKey = KeyController.instance.CheckKeyChain(requiredKey);
        
                if (hasKey)
                {
                    //Remove the key from the keyChain
                    KeyController.instance.RemoveKey(requiredKey);
            
                    //"Unlock" the container
                    containerObject.SetActive(false);
                    if (!contents.activeSelf)
                    {
                        contents.SetActive(true);
                    }
            
                    //TODO: Needs to be changed to an animation. Switch statement for different types of containers?
                }
                else
                {
                    //The player does not have the key
                    Debug.Log("The player does not have the key: " + requiredKey);
                }

                break;
            }
        }
    }
}
