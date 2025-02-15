using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyItem : MonoBehaviour, IInteraction
{
    [SerializeField] private string keyName;
    
    public virtual void Interact()
    {
            KeyController.instance.AddKey(keyName);
            Destroy(this.gameObject);
    }
}
