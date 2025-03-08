using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelTimeElapsed : MonoBehaviour
{
    public float timeElapsed;
    public TextMeshProUGUI timeText;
    public bool timerActive;
    
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    
    public void ResetTimer()
    {
        timeElapsed = 0;
    }

    public void Timer()
    {
        timeText.text = "In " + timeElapsed.ToString("F2") + ("s");
        if (timerActive)
        {
            timeElapsed += Time.deltaTime;
        }
        else if (!timerActive)
        {
            timeElapsed = timeElapsed;
        }
        
        if (LevelSequencer.Instance.isComplete)
        {
            timerActive = false;
        }
        else
        {
            timerActive = true;
        }
    }
}