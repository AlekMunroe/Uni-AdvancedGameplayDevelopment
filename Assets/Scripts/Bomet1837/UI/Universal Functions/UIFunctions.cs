using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// List of different methods for UI elements to access.
/// </summary>
public class UIFunctions : MonoBehaviour
{
    public void OpenUI(GameObject ui)
    {
        ui.SetActive(true);
    }
    
    public void CloseUI(GameObject ui)
    {
        ui.SetActive(false);
    }
    
    public void ToggleUI(GameObject ui)
    {
        ui.SetActive(!ui.activeSelf);
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

    public void OOB(GameObject player)
    {
        player.transform.position = new Vector3(0, 0, 0);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    
    public void Unpause()
    {
        Time.timeScale = 1;
    }
    
    public void TogglePause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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
