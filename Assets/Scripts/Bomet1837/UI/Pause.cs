using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
public GameObject pauseMenu;

private void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        UIFunctions.Instance.TogglePause();
        UIFunctions.Instance.ToggleUI(pauseMenu);
    }
}

}
