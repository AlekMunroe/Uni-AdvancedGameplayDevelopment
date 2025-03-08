using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSequencer : MonoBehaviour
{
    private UIFunctions _uiFunctions;
    public GameObject levelCompleteUI;
    [HideInInspector] public bool isComplete = false;
    public static LevelSequencer Instance;
    
    // Start is called before the first frame update
    void Start()
    {
        _uiFunctions = FindObjectOfType<UIFunctions>();
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isComplete = true;
            _uiFunctions.DisableControls();
            _uiFunctions.OpenUI(levelCompleteUI);
        }
    } 
}
