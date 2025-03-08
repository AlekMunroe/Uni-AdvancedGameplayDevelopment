using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private int currentLevel = 0;
    [SerializeField] private GameObject continueButton;

    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        if (currentLevel == 0)
        {
            //Hide the continue button
            continueButton.SetActive(false);
        }
    }
    public void NewGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueGameButton()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void ToggleSettings()
    {
        if (settingsPanel.activeInHierarchy)
        {
            mainPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
            mainPanel.SetActive(false);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
