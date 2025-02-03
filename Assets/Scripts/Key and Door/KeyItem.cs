using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IInteraction
{
    [SerializeField] private string keyName;
    
    public virtual void Interact()
    {
        KeyController.instance.addKey(keyName);

        Destroy(this.gameObject);
    }
}
