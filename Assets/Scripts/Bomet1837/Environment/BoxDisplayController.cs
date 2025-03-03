using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxDisplayController : MonoBehaviour
{
    [SerializeField] private Canvas _display;
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private Image _displayDecal;
    
    [SerializeField] private int _signalsRequired;
    [SerializeField] private BoxRequisiteDisplay_PressurePlate[] _signals;
    
    public bool _isActivated;
    public BoxDisplayController instance;

    void Start()
    {
        _display.enabled = true;
        _signalsRequired = _signals.Length;
        
        instance = this;
    }

    void Update()
    {
        CheckSignals();
        DisplayManager();
    }
    
    void DisplayManager()
    {
        if (_isActivated)
        {
            _displayDecal.color = Color.green;
            _counterText.color = Color.green;
        }
        else
        {
            _displayDecal.color = Color.red;
            _counterText.color = Color.red;
        }
    }
    
    void CheckSignals()
    {
        int signals = _signals.Length;

        
        
        for (int i = 0; i < _signals.Length; i++)
        {
            if (!_signals[i].signal)
            {
                signals -= 1;
            }
            
            
        }
        
       
        
        if (_signalsRequired == signals)
        {
            _isActivated = true;
        }
        else
        {
            _isActivated = false;
        }
        
        _counterText.text = signals + "/" + _signalsRequired;
        
    }
    
}
