using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

public class CameraTriggerInterface: MonoBehaviour, IInteraction
{
    [Header("Camera Settings")]
    [Tooltip("The index of the camera that will be used.")]
    private CinemachineVirtualCamera[] _cameras;
    [SerializeField] private int _currentCameraIndex = 0;

    private void Start()
    {
        _cameras = FindObjectOfType<TTCCinemachineVariant>().cameras;
    }

    public void Interact()
    {
        for (int i = 0; i < _cameras.Length; i++)
        { 
            _cameras[i].Priority = 0; 
        } 
        _cameras[_currentCameraIndex].Priority = 10;
    }
}