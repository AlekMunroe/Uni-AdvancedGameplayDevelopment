using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum ControllerType
{
    Door,
    Misc,
}

public class PowerBoxController : MonoBehaviour, IInteraction
{
    public SignalEmitter_PressurePlate[] plateSignals;
    public Light statusLight;
    public Color
        activeColor = Color.green,
        inactiveColor = Color.red,
        errorColor = Color.yellow;
    public GameObject doorObject;

    private bool _isActive = false;
    private bool _hasBattery = false;
    private string _batteryName = "Battery";

    void Update()
    {
        if (plateSignals.All<SignalEmitter_PressurePlate>(x => x.signal == true))
        {
            statusLight.color = activeColor;
            _isActive = true;
        }
        else if (plateSignals.All<SignalEmitter_PressurePlate>(x => x.signal == false))
        {
            statusLight.color = inactiveColor;
            _isActive = false;
        }
        else
        {
            statusLight.color = errorColor;
            _isActive = false;
        }

        _hasBattery = KeyController.instance.CheckKeyChain(_batteryName);


    }

    public virtual void Interact()
    {
        if (_isActive && _hasBattery)
        {
            //Unlock the door
            doorObject.GetComponent<Animation>().Play("DoorOpenAnim");
            KeyController.instance.RemoveKey(_batteryName);
            this.GetComponent<BoxCollider>().enabled = false; //Stops re-triggering the door
        }
        else if (!_hasBattery)
        {
            Debug.Log("The player does not have the battery: " + _batteryName);
        }
        else if (!_isActive)
        {
            Debug.Log("The power box is not active.");
        }

    }

}
