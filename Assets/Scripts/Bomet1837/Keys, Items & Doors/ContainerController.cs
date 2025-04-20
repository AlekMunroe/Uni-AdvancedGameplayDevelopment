using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;


public class ContainerController : MonoBehaviour, IInteraction
{
    public enum ContType
    {
        Default,
        Lvl6_DisplayTrigger,
        PressurePlate_Trigger
    }

    
    [Header("Container Settings")]
    public string contName;
    public ContType contType;
    [SerializeField] private string requiredKey;
    [SerializeField] private bool needsKey;
    
    [Header("Container References")]
    [SerializeField] private GameObject containerObject;
    [SerializeField] private GameObject contents;
    
    [HideInInspector] public bool messageDisplayed = false;
    [HideInInspector] public bool wasItLocked = false;
    [HideInInspector] public bool wasItKeyless = false;
    [SerializeField] private SignalEmitter_PressurePlate[] signalEmitter;
    public bool willFindObjectOfTypeEmitter = true;
    

    public void Start()
    {
        if (!needsKey)
        {
            requiredKey = null;
        }

        if (contType == ContType.PressurePlate_Trigger)
        {
            if (willFindObjectOfTypeEmitter)
            {
                signalEmitter = FindObjectsOfType<SignalEmitter_PressurePlate>();
                if (signalEmitter == null)
                {
                    Debug.LogError("SignalEmitter_PressurePlate components not found");
                }
            }
            else
            {
                Debug.LogError("No SignalEmitter_PressurePlate component found on " + gameObject.name);
                Debug.LogWarning("Emitter component auto assignment disabled, please assign it manually!");
            }

        }
        
        
    }
    
    public void Update()
    {
        if (contType == ContType.Lvl6_DisplayTrigger)
        {
            TriggeredActiviation();
        }
    }

    public virtual void Interact()
    {
        if (contType == default)
        {
            switch (requiredKey)
            {
                case null when !needsKey:
                {
                    //No key required for this container
                    wasItKeyless = true;
                    containerObject.SetActive(false);
                    if (!contents.activeSelf)
                    {
                        contents.SetActive(true);
                    }

                    break;
                }
                case null when needsKey:
                    Debug.LogError("Error: No key was set for the container: " + contName +
                                   ". Please set a key or disable the needsKey option.");
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
                        wasItLocked = true;
                        Debug.Log("The player does not have the key: " + requiredKey);
                    }

                    break;
                }
            }
        }
        else if (contType == ContType.Lvl6_DisplayTrigger)
        {
            Debug.Log(OnLevelDisplaysAtReqValue.instance._dispsActivatedBool.All(x => x == true)
                ? "All displays are activated"
                : "Not all displays are activated.");
        }
        else if (contType == ContType.PressurePlate_Trigger)
        {
            if (signalEmitter != null)
            {
                if (signalEmitter.All(x => x.signal == true))
                {
                    // The signals are all active, so we can activate the container
                    containerObject.SetActive(false);
                    if (!contents.activeSelf)
                    {
                        contents.SetActive(true);
                    }
                }
                else
                {
                    // The signals are not all active, so we can deactivate the container
                    containerObject.SetActive(true);
                    if (contents.activeSelf)
                    {
                        contents.SetActive(false);
                    }
                }
            }
        }
    }

    public void TriggeredActiviation()
    {
        if (contType == ContType.Lvl6_DisplayTrigger)
        {
            if (OnLevelDisplaysAtReqValue.instance._dispsActivatedBool.All(x => x == true))
            {
                containerObject.SetActive(false);
                if (!contents.activeSelf)
                {
                    contents.SetActive(true);
                }
            }
            else
            {
                containerObject.SetActive(true);
                if (contents.activeSelf)
                {
                    contents.SetActive(false);
                }
            }
        }
    }
}
