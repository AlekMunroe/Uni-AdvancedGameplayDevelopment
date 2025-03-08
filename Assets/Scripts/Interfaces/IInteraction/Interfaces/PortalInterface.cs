using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(BoxCollider))]
public class PortalInterface : MonoBehaviour, IInteraction
{
    [SerializeField] private Camera oldCamera;
    [SerializeField] private Camera newCamera;
    [Tooltip("How far the player moves along the X axis.")] 
    [SerializeField] private float travelLocation = 1000;

    public BoxCollider boxCollider; //Used for drawing the box of the travel location
    
    
    
    public virtual void Interact()
    {
        TimeTravelController.Instance.TimeTravel(oldCamera, newCamera, travelLocation);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        //Used to help calculate the travel location
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        if (boxCollider != null)
        {
            //Draw the cube
            Gizmos.color = Color.red;
            Vector3 newPosition = new Vector3(this.transform.position.x + travelLocation, this.transform.position.y, this.transform.position.z);
            Gizmos.DrawWireCube(newPosition + boxCollider.center, boxCollider.size);
            
            //Draw the text
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 12;

            Handles.Label(newPosition + boxCollider.center, "Portal Destination", style);
        }
    }
#endif
}
