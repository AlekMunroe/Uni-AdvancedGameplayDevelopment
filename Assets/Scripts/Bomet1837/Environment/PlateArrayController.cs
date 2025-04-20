using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlateArrayController : MonoBehaviour
{
    private SignalEmitter_PressurePlate[][] _plateSignalsArray;
    private bool[][] _plateSignalsBoolArray;
    private bool[][] _solutionArray;
    private GameObject [][] _plateDisplayLightsArray;

    void Awake()
    {
        CreateSolutionArray();
        _plateSignalsArray = new SignalEmitter_PressurePlate[5][];
        for (int i = 0; i < 6; i++)
        {
            _plateSignalsArray[i] = new SignalEmitter_PressurePlate[3];
        }

        _plateDisplayLightsArray = new GameObject[5][];
        for (int i = 0; i < 6; i++)
        {
            _plateDisplayLightsArray[i] = new GameObject[3];
        }

        List<SignalEmitter_PressurePlate> plateSignals = new List<SignalEmitter_PressurePlate>(FindObjectsOfType<SignalEmitter_PressurePlate>());

        foreach (var plateSignal in plateSignals)
        {
            string name = plateSignal.name;

            if (name.Length == 2)
            {
                char rowChar = name[0];
                int col = int.Parse(name[1].ToString());

                int row = rowChar - 'A';

                if (row >= 0 && row < 6 && col >= 0 && col < 4)
                {
                    _plateSignalsArray[row][col] = plateSignal;
                }
            }
        }

        List<GameObject> plateDisplayLights = new List<GameObject>(GameObject.FindGameObjectsWithTag("DisplayLight"));

        foreach (var plateDisplayLight in plateDisplayLights)
        {
            string name = plateDisplayLight.name;

            if (name.Length == 2)
            {
                char rowChar = name[0];
                int col = int.Parse(name[1].ToString());

                int row = rowChar - 'A';

                if (row >= 0 && row < 6 && col >= 0 && col < 4)
                {
                    _plateDisplayLightsArray[row][col] = plateDisplayLight;
                }
            }
        }


    }

    void CreateSolutionArray()
    {
        _solutionArray = _plateSignalsBoolArray;

        foreach (var solutionSignal in _solutionArray)
        {
            solutionSignal[Random.Range(0, 3)] = true;
        }

    }



    void Update()
    {
        foreach (var plateSignal in _plateSignalsArray)
        {
            foreach (var signal in plateSignal)
            {
                if (signal.signal == true)
                {
                    int row = Array.IndexOf(_plateSignalsArray, plateSignal);
                    int col = Array.IndexOf(plateSignal, signal);

                    int solutionRow = Array.IndexOf(_solutionArray, _plateSignalsBoolArray[row]);
                    int solutionCol = Array.IndexOf(_solutionArray[solutionRow], _plateSignalsBoolArray[row][col]);

                    if (solutionCol == col && solutionRow == row && _solutionArray[row][col] == true)
                    {
                        _plateDisplayLightsArray[row][col].GetComponent<Renderer>().material.color = Color.green;
                    }
                    else
                    {
                        _plateDisplayLightsArray[row][col].GetComponent<Renderer>().material.color = Color.red;
                    }

                }
            }
        }

    }
}
