using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                Debug.Log("Player has collided with " + gameObject.name);
                break;
            case "Interactable":
                Debug.Log("Interactable has collided with " + gameObject.name);
                break;
            default:
                Debug.Log("Something has collided with " + gameObject.name);
                break;
        }
    }
}
