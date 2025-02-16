using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private float requiredWeight;
    [SerializeField] private string collisionTag;
    private IActivatable _activation;

    void Start()
    {
        _activation = this.GetComponent<IActivatable>();
    }
    void OnCollisionEnter(Collision other)
    {
        //These are seperate if statements if we want to add something wihtout the weight
        if (other.gameObject.tag == collisionTag) //If it is the correct tag
        {
            if (other.gameObject.GetComponent<ObjectProperties>().weight() >= requiredWeight) //If it is the correct weight
            {
                //Activate the script
                _activation.Activate();
            }
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
