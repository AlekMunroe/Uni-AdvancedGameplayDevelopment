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
        musicFuture.Play(); musicPast.Play();
        if (playerTimeline != null) { musicPast.volume = 100; musicFuture.volume = 0; }

    }

    private void Update()
    {
        if(playerTimeline != null)
        {
        if (playerTimeline.currentTimeline == 1)
        {
            if (musicFuture.isPlaying)
            {
                StartCoroutine(FadeOut(musicFuture));
                if (!musicPast.isPlaying)
                {
                    StartCoroutine(FadeIn(musicPast));
                }
            }
        
        }
        else if (playerTimeline.currentTimeline == 2)
        {
            if(musicPast.isPlaying)
            {
                StartCoroutine(FadeOut(musicPast));
                if (!musicFuture.isPlaying)
                {
                    StartCoroutine(FadeIn(musicFuture));
                }
            }
        }
       }
       else
       {
           Debug.LogError("TimelineIdentifier not found in the scene.");
       }

       if (UIFunctions.Instance != null)
       {
              if (UIFunctions.Instance._isPaused)
              {
                Debug.Log("Music should be paused.");
                musicPast.Pause();
                musicFuture.Pause();
              }
              else
              {
                Debug.Log("Music should be unpaused.");
                musicPast.UnPause();
                musicFuture.UnPause();
              }
         }
         else
         {
            Debug.LogError("UI Functions script not found in the scene.");
              }
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

