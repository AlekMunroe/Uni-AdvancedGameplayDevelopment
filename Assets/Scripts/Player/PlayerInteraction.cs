using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script wil be used to allow the player to interact with objects if they are in front of them
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    private bool isEDown;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !TTCCinemachineVariant.Instance.IsTravellingTime())
        {
            isEDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.L) || TTCCinemachineVariant.Instance.IsTravellingTime())
        {
            isEDown = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            //Get and call the interact script
            IInteraction interaction = other.GetComponent<IInteraction>();
            interaction.Interact();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            if (isEDown)
            {
                //Debug.Log("Has pressed push button");
                other.GetComponent<PushableBlock>().Push();
            }
        }
    }
}
