using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class ToggleFPLightCam : MonoBehaviour
{
    [Header("Camera Settings")]
    [HideInInspector] public bool _camToggle;
    public CinemachineVirtualCamera firstPersonCam;
    public Camera camera;
    public GameObject player;
    
    public static ToggleFPLightCam instance;
    private CinemachinePOV _torchPOV;
    [HideInInspector] public bool _hasTorch = false;
    private UIFunctions _uiFunctions;
    
    [Header("Torch Control Settings")]
    public GameObject torch;
    public Light torchLight;
    
    
    
    void Start()
    {
        _uiFunctions = FindObjectOfType<UIFunctions>();
        _torchPOV = firstPersonCam.GetCinemachineComponent<CinemachinePOV>();
        torch.SetActive(false);
        instance = this;
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _hasTorch)
        {
            _camToggle = !_camToggle;
        }
        
        if (_camToggle)
        {
            HandleCameraControls();
            _uiFunctions.DisableControls();
            torch.SetActive(true);
            
        }
        else
        {
            _uiFunctions.EnableControls();
            torch.SetActive(false);
        }

        
    }
    
    void LateUpdate()
    {
        if (_camToggle)
        {
            firstPersonCam.Priority = 20;
            torchLight.enabled = true;
            HandleTorchControls();
            
            
        }
        else
        {
            firstPersonCam.Priority = 0;
            torchLight.enabled = false;
        }
        
        
        
    }

    void HandleTorchControls()
    {

        if (_camToggle)
        {
            torch.transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.x, camera.transform.rotation.eulerAngles.y +180f, 0f);
            Mathf.Clamp(torch.transform.rotation.eulerAngles.y, -90 - 180f, 90 - 180f);
        }
    }

    void HandleCameraControls()
    {
        Vector3 kbInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _torchPOV.m_HorizontalAxis.Value += kbInput.x;
        _torchPOV.m_VerticalAxis.Value += kbInput.z;
    }

}
