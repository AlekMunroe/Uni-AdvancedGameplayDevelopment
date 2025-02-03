using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [Header("StartScreens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject startCanvas;

    [Header("Load bar")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelButton(string levelBeingLoaded)
    {
        startCanvas.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(levelBeingLoaded));
    }

    IEnumerator LoadLevelASync(string levelBeingLoaded)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelBeingLoaded);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress/0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}