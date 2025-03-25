using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlateType { Box, Player }

public class PressurePlate : MonoBehaviour
{
    [Header("Settings")]
    
    [Tooltip("The weight required to trigger the plate. Only used if the plate type is Box.")]
    [SerializeField] private float requiredWeight;
    
    [Tooltip("The tag of the object that will trigger the plate. Only used if the plate type is Box.")]
    [SerializeField] private string collisionTag;
    
    [Tooltip("The type of object that will trigger the plate.")]
    public PlateType plateType;
    
    private IActivatable _activation;

    void Start()
    {
        _activation = this.GetComponent<IActivatable>();
    }
    void OnCollisionEnter(Collision other)
    {
        //These are seperate if statements if we want to add something wihtout the weight
        if (other.gameObject.tag == collisionTag && plateType == PlateType.Box) //If it is the correct tag
        {
            if (other.gameObject.GetComponent<ObjectProperties>().weight() >= requiredWeight) //If it is the correct weight
            {
                //Activate the script
                _activation.Activate();
            }
        }
        else if (other.gameObject.tag == "Player" && plateType == PlateType.Player) //If it is the player
        {
            //Activate the script
            _activation.Activate();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == collisionTag)
        {
            //Deactivate the script
            _activation.Deactivate();
        }
    }
}
