using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
public GameObject pauseMenu;
public UIFunctions uiFunctions;

private void Start()
{
    uiFunctions = GetComponent<UIFunctions>();
}

private void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        uiFunctions.TogglePause();
        uiFunctions.ToggleUI(pauseMenu);
    }
}
}
