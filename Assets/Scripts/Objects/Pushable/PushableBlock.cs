using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControls;

[RequireComponent(typeof(ObjectProperties))]
public class PushableBlock : MonoBehaviour
{
    // updated these variables for reference in the BoxInteract script
    [HideInInspector] public bool canMoveX;
    [HideInInspector] public bool canMoveZ;
    

    public KeyCode pushKey = PlayerControls.PlayerControls.pushKey;
    
    private Transform playerPushPosition;
    private Vector3 startPosition;

    private bool isPushing;

    void Start()
    {
        playerPushPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerPushPosition;
        startPosition = transform.position;
    }

    void Update()
    {
        if (isPushing)
        {
            PushThis();
        }

        if (Input.GetKeyUp(PlayerControls.PlayerControls.pushKey) || TimeTravelController.Instance.isTravellingTime())
        {
            isPushing = false;
        }
    }

    public void Push()
    {
        isPushing = true;
    }

    private void PushThis()
    {
        float newX = this.transform.position.x;
        float newZ = this.transform.position.z;
        
        if (canMoveX && !canMoveZ)
        {
            //Move on the X axis
            newX = playerPushPosition.position.x;
            newZ = startPosition.z; //Lock the z axis
        }
        else if (!canMoveX && canMoveZ)
        {
            newX = startPosition.x; //Lock the x acis
            newZ = playerPushPosition.position.z;
        }
        else if(canMoveX && canMoveZ)
        {
            //Move on the X and Z axis
            //this.transform.position = new Vector3(playerPushPosition.position.x, this.transform.position.y,
                //playerPushPosition.position.z);
            newX = playerPushPosition.position.x;
            newZ = playerPushPosition.position.z;
        }
        else
        {
            Debug.LogWarning("canMoveX and canMoveZ are false. This block will not work");
        }
        
        //Actually move
        this.transform.position = new Vector3(newX, this.transform.position.y, newZ);
    }
    
    public void Lock()
    {
        canMoveX = false;
        canMoveZ = false;
    }
    
    public void Unlock()
    {
        canMoveX = true;
        canMoveZ = true;
    }
}