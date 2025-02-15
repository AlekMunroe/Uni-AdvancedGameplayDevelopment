using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will hold information about what keys you currently have available
/// </summary>
public class KeyController : MonoBehaviour
{
    public static KeyController instance;
    [SerializeField] private List<string> keyChain;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddKey(string keyName)
    {
        //Add the key to the list
        keyChain.Add(keyName);
        
        //TODO: Add a check if the key already exist
    }

    public void RemoveKey(string keyName)
    {
        //Remove the key from the list
        keyChain.Remove(keyName);
    }
    
    public bool CheckKeyChain(string keyName)
    {
        //Check if the player has the correct key in the inventory
        foreach (var t in keyChain)
        {
            if (t == keyName)
            {
                Debug.Log("Has key: " + keyName);

                //Key exists
                return true;
            }
        }
        
        //Key does not exist
        return false;
    }
}
