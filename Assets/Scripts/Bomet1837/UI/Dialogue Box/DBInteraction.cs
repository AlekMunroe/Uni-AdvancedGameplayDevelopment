using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBInteraction: MonoBehaviour
{
    public bool isInteractable = false;
    
    public GameObject dBoxUI;
    public List<MsgSystem> dialogues = new List<MsgSystem>();
    
    public KeyCode interactKey = KeyCode.E;
    
    private DBController dbController;
    
    private void Start()
    {
        dbController = dBoxUI.GetComponent<DBController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }
        
        Vector3[] directions =
        {
            Quaternion.Euler(0, 45, 0) * -transform.forward,
            Quaternion.Euler(0, -45, 0) * -transform.forward,
            -transform.forward,
        };
        
        foreach (Vector3 direction in directions)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, direction, Color.red);
            if (Physics.Raycast(transform.position, direction, out hit, 2))
            {

                isInteractable = true;

            }
            else
            {
                isInteractable = false;
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Vector3[] directions =
        {
            Quaternion.Euler(0, 45, 0) * -transform.forward,
            Quaternion.Euler(0, -45, 0) * -transform.forward,
            -transform.forward,
        };
        
        Gizmos.color = Color.red;
        foreach (Vector3 direction in directions)
        {
            Gizmos.DrawRay(transform.position, direction * 2);
        }
    }

    private void Interact()
    {
        if (isInteractable && dbController.dialogues.Count == 0)
        {
            foreach (MsgSystem dialogue in dialogues)
            {
                dbController.AddDialogue(dialogue);
            }

            dbController.ShowBox();
        }
    }
}