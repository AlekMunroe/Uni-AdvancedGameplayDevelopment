using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DisplayType
{
    Image,
    Light,
}

public class AlternateDisplaySignalReceiver : MonoBehaviour
{
    public DisplayType displayType;
    private Image _displayImage;
    private Light _displayLight;
    [SerializeField] private SignalEmitter_PressurePlate _signalEmitter;

    private Color
        _activeColor = Color.green,
        _inactiveColor = Color.red;

    public bool willFindObjectOfTypeEmitter = true;


    // Awake is called when the script instance is being loaded.
    // Use this for initialization
    void Awake()
    {
        switch(displayType)
        {
            case DisplayType.Image:
            _displayImage = GetComponentInChildren<Image>();
            if (_displayImage == null)
            {
                Debug.LogError("No Image component found in children of " + gameObject.name);
            }
            break;

            case DisplayType.Light:
            _displayLight = GetComponentInChildren<Light>();
            if (_displayLight == null)
            {
                Debug.LogError("No Light component found in children of " + gameObject.name);
            }
            break;
        }

        if (willFindObjectOfTypeEmitter)
            {
                _signalEmitter = FindObjectOfType<SignalEmitter_PressurePlate>();
            }
        else
            {
                Debug.LogError("No SignalEmitter_PressurePlate component found on " + gameObject.name);
                Debug.LogWarning("Emitter component auto assignment disabled, please assign it manually!");
            }
    }

    void Start()
    {
        _displayImage.color = new Color(0,0,0,1);   
    }

    // Update is called once per frame
    void Update()
    {
        switch (displayType)
        {
            case DisplayType.Image:
                if (_signalEmitter.signal == true)
                {
                    _displayImage.color = _activeColor;
                }
                else if (_signalEmitter.signal == false)
                {
                    _displayImage.color = _inactiveColor;
                }
                break;

            case DisplayType.Light:
                if (_signalEmitter.signal == true)
                {
                    _displayLight.color = _activeColor;
                }
                else if (_signalEmitter.signal == false)
                {
                    _displayLight.color = _inactiveColor;
                }
                break;
        }
        
        
        
    }
}
