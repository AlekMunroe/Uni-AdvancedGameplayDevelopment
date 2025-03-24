using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// List of different methods for UI elements to access.
/// </summary>
public class UIFunctions : MonoBehaviour
{
    
    public AudioSource
    uiConfirmPrompt,
    uiSelect,
    uiCancel,
    uiHover,
    uiOpen,
    uiClose;

    Canvas[] canvasSearch;
    


    public void Start()
    {
        canvasSearch = FindObjectsOfType<Canvas>();

        uiConfirmPrompt = GameObject.Find("UIConfirmPrompt").GetComponent<AudioSource>();
        uiSelect = GameObject.Find("UISelect").GetComponent<AudioSource>();
        uiCancel = GameObject.Find("UICancel").GetComponent<AudioSource>();
        uiHover = GameObject.Find("UIHover").GetComponent<AudioSource>();
        uiOpen = GameObject.Find("UIOpen").GetComponent<AudioSource>();
        uiClose = GameObject.Find("UIClose").GetComponent<AudioSource>();
        
    }

    public void Update()
    {
        // CheckPaused();
        Debug.Log(Time.timeScale);
    }

    public void OnButtonEnter(BaseEventData eventData)
    {
        Hover();
    }

    public void CheckPaused()
    {
        for (var c = 0; c < canvasSearch.Length; c++)
        {
            if (Time.timeScale == 0 && canvasSearch[c].enabled == false)
            {
                Unpause();
            }
        }
    }
    
    public void ConfirmPrompt()
    {
        uiConfirmPrompt.Play();
    }
    
    public void Select()
    {
        uiSelect.Play();
    }
    
    public void Cancel()
    {
        uiCancel.Play();
    }
    
    public void Hover()
    {
        uiHover.Play();
    }
    

    public void OpenUI(GameObject ui)
    {
        ui.SetActive(true);
        uiOpen.Play();
        
    }
    
    public void CloseUI(GameObject ui)
    {
        if (ui.activeSelf)
        {
            uiClose.Play();
        }
        
        ui.SetActive(false);
        Unpause();
        
        
    }
    
    public void ToggleUI(GameObject ui)
    {
        ui.SetActive(!ui.activeSelf);
        if (ui.activeSelf)
        {
            uiOpen.Play();
        }
        else
        {
            uiClose.Play();
        }
    }
    
    public void EnableControls()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
    }
    
    public void DisableControls()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
    }
    
    public void ToggleControls()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled;
    }

    public void OOB(GameObject player, Vector3 position)
    {
        if (player.CompareTag("Player"))
        {
            PlayerController.Instance.FreezePlayer(0.5f);
        }

        player.transform.position = position;
    }
    
    

    public void Pause()
    {
        if (PlayerController.Instance != null && PlayerController.Instance._controller != null)
        {
            PlayerController.Instance._controller.enabled = false;
        }
        else
        {
            Debug.LogError("Player Controller not found!");
        }

        Time.timeScale = 0;
        if (Time.timeScale != 0)
        {
            Debug.LogError("Time.timeScale is not 0 after using the pause method!");
        }
        
    }
    
    public void Unpause()
    {
        if (PlayerController.Instance != null && PlayerController.Instance._controller != null)
        {
            PlayerController.Instance._controller.enabled = true;
        }
        else
        {
            Debug.LogError("Player Controller not found!");
            
        }

        Time.timeScale = 1;
        if (Time.timeScale != 1)
        {
            Debug.LogError("Time.timeScale is not 1 after using the unpause method!");
        }
    }
    
    public void TogglePause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        PlayerController.Instance._controller.enabled = !PlayerController.Instance._controller.enabled;
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Unpause();
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        Unpause();
    }
    

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    
    
}
