using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
public GameObject pauseMenu;
public UIFunctions uiFunctions;

public bool isPaused = false;

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

    private void FixedUpdate()
    {
        if (pauseMenu.activeSelf)
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
        }

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
