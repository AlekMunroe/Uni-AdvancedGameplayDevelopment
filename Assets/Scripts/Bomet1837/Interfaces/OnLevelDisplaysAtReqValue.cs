using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnLevelDisplaysAtReqValue : MonoBehaviour
{
    public static OnLevelDisplaysAtReqValue instance;
    
    private BoxDisplayController[] _dispsActivated;
    [HideInInspector] public bool[] _dispsActivatedBool;

    void Start()
    {
        _dispsActivated = FindObjectsOfType<BoxDisplayController>() ;
        _dispsActivatedBool = new bool[_dispsActivated.Length];
        instance = this;
    }
    
    public void Update()
    {
        CheckDisplays();
    }

    public void CheckDisplays()
    {
        for (var d = 0; d < _dispsActivated.Length; d++)
        {
            if (_dispsActivated[d]._isActivated)
            {
                _dispsActivatedBool[d] = true;
            }
            else 
            {
                _dispsActivatedBool[d] = false;
            }
        }
    }

 
}
