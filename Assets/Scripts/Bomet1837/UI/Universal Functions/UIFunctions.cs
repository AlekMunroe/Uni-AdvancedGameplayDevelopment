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
    public static UIFunctions Instance;
    
    public AudioSource
    uiConfirmPrompt,
    uiSelect,
    uiCancel,
    uiHover,
    uiOpen,
    uiClose;


    [HideInInspector] public bool _isPaused;
    
    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }


        uiConfirmPrompt = GameObject.Find("UIConfirmPrompt").GetComponent<AudioSource>();
        uiSelect = GameObject.Find("UISelect").GetComponent<AudioSource>();
        uiCancel = GameObject.Find("UICancel").GetComponent<AudioSource>();
        uiHover = GameObject.Find("UIHover").GetComponent<AudioSource>();
        uiOpen = GameObject.Find("UIOpen").GetComponent<AudioSource>();
        uiClose = GameObject.Find("UIClose").GetComponent<AudioSource>();
        
    }

    public void Update()
    {
        if (_isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            if (Time.timeScale != 1)
            {
                Debug.LogWarning("Time.timeScale is not 1.");
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
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            uiHover.Play();
        }
        else
        {
            Debug.LogWarning("No GameObject selected.");
        }
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
    }

    public void CloseMenu(GameObject menu)
    {
        if (menu.activeSelf)
        {
            uiClose.Play();
        }
        
        menu.SetActive(false);
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
            StartCoroutine(PlayerController.Instance.FreezePlayer(0.1f));
            player.transform.position = position;
        }

        player.transform.position = position;
    }
    
    

    public void Pause()
    {
        _isPaused = true;
    }
    
    public void Unpause()
    {
        _isPaused = false;
    }
    
    public void TogglePause()
    {
        _isPaused = !_isPaused;
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
