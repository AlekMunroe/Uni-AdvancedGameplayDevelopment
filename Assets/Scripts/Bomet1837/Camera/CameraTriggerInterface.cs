using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

public class CameraTriggerInterface: MonoBehaviour
{
    [Header("Camera Settings")]
    [Tooltip("The index of the camera that will be used.")]
    [SerializeField] private CinemachineVirtualCamera[] _cameras;
    [SerializeField] private int _currentCameraIndex = 0;
    [SerializeField] private  bool justSwitched = false;

    private void Start()
    {
        _cameras = FindObjectOfType<TTCCinemachineVariant>().cameras;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(OnTriggerEnterCoroutine(other));
    }
    
    private IEnumerator OnTriggerEnterCoroutine(Collider other)
    {

        if (other.CompareTag("Player") && !justSwitched)
        {
            for (int i = 0; i < _cameras.Length; i++)
            {
                    { 
                        _cameras[i].Priority = 0; 
                    } 
                    _cameras[_currentCameraIndex].Priority = 10;
                    yield return new WaitForSeconds(0.1f);
                    justSwitched = true; 
            }
        }
    }
    
    private IEnumerator ResetJustSwitched()
    {
        yield return new WaitForSeconds(2f); // Adjust the delay time as needed
        justSwitched = false;
    }

    
    
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ResetJustSwitched());
        }
    }
}