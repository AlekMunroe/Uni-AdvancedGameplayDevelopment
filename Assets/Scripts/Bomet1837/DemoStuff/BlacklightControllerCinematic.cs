using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BlacklightControllerCinematic : MonoBehaviour
{
    public Light spotlight;
    public Material bl;
    
    private void Update()
    {
            HandleBlacklight();
    }
    
    private void HandleBlacklight()
    {

            bl.SetVector("_LightPos", spotlight.transform.position);
            bl.SetVector("_LightDir", -spotlight.transform.forward);
            bl.SetFloat("_LightAngle", spotlight.spotAngle);
            bl.SetFloat("StrengthScalar", spotlight.intensity);
    }
    
    
}
