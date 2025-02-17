using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LeverInteraction : MonoBehaviour
{
    private bool _isPlayer;
    [SerializeField] private bool _isLeverOn;
    [SerializeField] private GameObject leverObject;
    private bool _previousLeverState;
    private IActivatable _activation;

    void Start()
    {
        _previousLeverState = _isLeverOn;
        _activation = this.GetComponent<IActivatable>();
    }

    void Update()
    {
        //Wait for input
        if (_isPlayer && Input.GetKeyDown(KeyCode.P))
        {
            _isLeverOn = !_isLeverOn;
        }

        if (_previousLeverState != _isLeverOn)
        {
            _previousLeverState = _isLeverOn;

            if (_isLeverOn)
            {
                //Do something
                leverObject.GetComponent<Animation>().Play("LeverSwitch_On");
                _activation.Activate();
            }
            else
            {
                //Do something else
                leverObject.GetComponent<Animation>().Play("LeverSwitch_Off");
                _activation.Deactivate();
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlayer = true;
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlayer = false;
        }
    }
}
