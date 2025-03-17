using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicDirector : MonoBehaviour
{
    [SerializeField] private AudioSource musicPast, musicFuture ; 
    private TimelineIdentifier playerTimeline;
    public float fadeDuration = 1.0f;

    private void Awake()
    {
        playerTimeline = FindObjectOfType<TimelineIdentifier>();   
    }

    private void Update()
    {
        //if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Demo_MainMenu")
       // {
        if (playerTimeline.currentTimeline == 1)
        {
            if (musicFuture.isPlaying)
            {
                StartCoroutine(FadeOut(musicFuture));
                StartCoroutine(FadeIn(musicPast));
            }
        
        }
        else if (playerTimeline.currentTimeline == 2)
        {
            if(musicPast.isPlaying)
            {
                StartCoroutine(FadeOut(musicPast));
                StartCoroutine(FadeIn(musicFuture));
            }
        }
       // }
    }

    private IEnumerator FadeIn(AudioSource audioSource)
    {
        audioSource.Play();
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = t / fadeDuration;
            yield return null;
        }
        audioSource.volume = 1;
    }

    private IEnumerator FadeOut(AudioSource audioSource)
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = 1 - (t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }
}

