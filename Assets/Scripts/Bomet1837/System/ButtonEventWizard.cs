using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A window that creates event triggers for all buttons in the scene. *WIP*
/// </summary>
public class ButtonEventWizard : EditorWindow
{
    [MenuItem("Tools/Bomet1837/Button Event Wizard")]
    public static void ShowWindow()
    {
        GetWindow<ButtonEventWizard>("Button Event Wizard");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Create Event Triggers for All Buttons"))
        {
            CreateEventTriggers();
        }
    }

    private void CreateEventTriggers()
    {
        
    }


}
