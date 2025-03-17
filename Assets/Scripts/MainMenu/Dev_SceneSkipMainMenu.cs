using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dev_SceneSkipMainMenu : MonoBehaviour
{
    public void SkipToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
