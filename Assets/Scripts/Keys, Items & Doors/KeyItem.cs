using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyItem : MonoBehaviour, IInteraction
{
    public string keyName;
    [HideInInspector] public bool messageDisplayed = false;
    
    public virtual void Interact()
    {
            KeyController.instance.AddKey(keyName);
            Destroy(this.gameObject);
    }
}
