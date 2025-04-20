using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// This script will handle unlocking the door
/// </summary>
public class DoorController : MonoBehaviour, IInteraction
{
    [SerializeField] private string requiredKey;
    [HideInInspector] public bool isDoorLocked = true;
    [SerializeField] private GameObject doorObject;
    
    [HideInInspector] public bool messageDisplayed = false;
    [HideInInspector] public bool wasItLocked = false;
    public bool isOneWay = false;

    void Start()
    {
        if (requiredKey == "")
        {
            requiredKey = null;
        }
    }

    public virtual void Interact()
    {
        bool hasKey = KeyController.instance.CheckKeyChain(requiredKey);

        if (hasKey)
        {
            //Unlock the door
            isDoorLocked = false;
            
            //Remove the key from the keyChain
            KeyController.instance.RemoveKey(requiredKey);

            this.GetComponent<BoxCollider>().enabled = false; //Stops re-triggering the door

            switch (requiredKey)
            {
                default:
                    doorObject.GetComponent<Animation>().Play("DoorOpenAnim");
                    break;
                
                //for vent door
                case "Key_Screwdriver":
                    doorObject.SetActive(false);
                    break;
                
                //TODO: Vent door animation needed
            }
            
        }
        else
        {
            //The player does not have the key
            Debug.Log("The player does not have the key: " + requiredKey);
            wasItLocked = true;

            if (requiredKey == null || requiredKey == "")
            {
               Debug.Log("No key required for this door, or key has not been set. Opening door anyway.");
               doorObject.SetActive(false);
               this.gameObject.GetComponent<BoxCollider>().enabled = false; //Stops re-triggering the door
            }
        }
    }


    public void Update()
    {
        if (isOneWay && doorObject.activeSelf == false)
        {
            StartCoroutine(ResetDoor());
        }
    }

    public IEnumerator ResetDoor()
    {
        yield return new WaitForSeconds(3f);
        doorObject.SetActive(true);
        this.gameObject.GetComponent<BoxCollider>().enabled = true; //Stops re-triggering the door
    }    
}
