using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnLevelDisplaysAtReqValue : MonoBehaviour
{
    public GameObject door;
    
    private BoxDisplayController[] _dispsActivated;
    private bool[] _dispsActivatedBool;

    void Start()
    {
        _dispsActivated = FindObjectsOfType<BoxDisplayController>() ;
        _dispsActivatedBool = new bool[_dispsActivated.Length];
    }
    
    public void Update()
    {
        CheckDisplays();
    }

    public void CheckDisplays()
    {
        foreach (var d in _dispsActivated)
        {
            if (d._isActivated)
            {
                _dispsActivatedBool[_dispsActivated.Length] = true;
            }
            else 
            {
                _dispsActivatedBool[_dispsActivated.Length] = false;
            }
        }
        
        if (_dispsActivatedBool.All(x => x == true))
        {
            door.SetActive(false); 
        }
        else
        { 
            door.SetActive(true);
        }


        
        
        

    }

 
}
