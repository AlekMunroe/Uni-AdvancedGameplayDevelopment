using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    [SerializeField] private bool canMoveX;
    [SerializeField] private bool canMoveZ;
    
    private Transform playerPushPosition;

    private bool isPushing;

    void Start()
    {
        playerPushPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerPushPosition;
    }

    void Update()
    {
        if (isPushing)
        {
            PushThis();
        }

        if (Input.GetKeyUp(KeyCode.E))
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
        if (canMoveX && !canMoveZ)
        {
            //BUG: Moving on one axis move the cube incorrectly. This will need fixing.
            Debug.LogWarning("Locking one axis is glitchy. This will need fixing.");
            //Move on the X axis
            this.transform.position = new Vector3(playerPushPosition.position.x, this.transform.position.y,
                this.transform.position.z);
        }
        else if (!canMoveX && canMoveZ)
        {
            Debug.LogWarning("Locking one axis is glitchy. This will need fixing.");
            //Move on the Z axis
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,
                playerPushPosition.position.z);
        }
        else if(canMoveX && canMoveZ)
        {
            //Move on the X and Z axis
            this.transform.position = new Vector3(playerPushPosition.position.x, this.transform.position.y,
                playerPushPosition.position.z);
        }
        else
        {
            Debug.LogWarning("canMoveX and canMoveZ are false. This block will not work");
        }
    }
}