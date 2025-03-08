using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This script will handle the change in cameras
/// </summary>
public class TTCCinemachineVariant : MonoBehaviour
{
    public static TTCCinemachineVariant Instance;
    
    [Tooltip("The cameras that will be cycled through.")]
    public CinemachineVirtualCamera[] cameras;
    [SerializeField] private GameObject fadeUI;
    private GameObject _playerObject;
    private bool _isTravelling, _justSwitched;

    void Start()
    {
        _playerObject = GameObject.FindGameObjectWithTag("Player");

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void TimeTravel(int currentCameraIndex, float travelLocation)
    {
        StartCoroutine(TransitionUI(currentCameraIndex, travelLocation));
    }
    
    IEnumerator IfJustSwitched()
    {
        _justSwitched = true;
        yield return new WaitForSeconds(1.5f);
        _justSwitched = false;
    }

    IEnumerator TransitionUI(int currentCameraIndex, float travelLocation)
    {
        _isTravelling = true;
        //Fade in
        fadeUI.SetActive(true);
        fadeUI.GetComponent<Animation>().Play("FadeInAnim");
        
        yield return new WaitForSeconds(0.4f);
        
        //Change the cameras
        
        if (!_justSwitched)
        {
            for (int i = 0; i < cameras.Length; i++)
            { 
                cameras[i].Priority = 0; 
            } 
            cameras[currentCameraIndex].Priority = 10;
        }
        
        
        StartCoroutine(IfJustSwitched());
        

        
        //Move the player (+travelLocation on the x)
        _playerObject.transform.position = new Vector3(_playerObject.transform.position.x + travelLocation,
            _playerObject.transform.position.y + 0.1f, _playerObject.transform.position.z);

        //Fade out
        fadeUI.GetComponent<Animation>().Play("FadeOutAnim");
        yield return new WaitForSeconds(0.4f);
        fadeUI.SetActive(false);
        _isTravelling = false;
    }

    public void ResetCamera()
    {
        foreach (var i in cameras)
        {
            i.Priority = 0;
        }

        cameras[0].Priority = 10;
    }

    public bool IsTravellingTime()
    {
        return _isTravelling;
    }
}
