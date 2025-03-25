using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateArrayController : MonoBehaviour
{
    private SignalEmitter_PressurePlate[][] _plateSignalsArray;
    private GameObject [][] _plateDisplayLightsArray;

    void Awake()
    {
        List<SignalEmitter_PressurePlate> plateSignals = new List<SignalEmitter_PressurePlate>();

        FindObjectsOfType<SignalEmitter_PressurePlate>();
        foreach (var plateSignal in plateSignals)
        {
            switch (plateSignal)
            {
                case var _ when plateSignal.name.StartsWith("A"):
                    _plateSignalsArray[0][0] = plateSignal;
                    break;
                case var _ when plateSignal.name.StartsWith("B"):
                    _plateSignalsArray[0][1] = plateSignal;
                    break;
                case var _ when plateSignal.name.StartsWith("C"):
                    _plateSignalsArray[0][2] = plateSignal;
                    break;
                case var _ when plateSignal.name.StartsWith("D"):
                    _plateSignalsArray[0][3] = plateSignal;
                    break;
            }
        }
    }
}
