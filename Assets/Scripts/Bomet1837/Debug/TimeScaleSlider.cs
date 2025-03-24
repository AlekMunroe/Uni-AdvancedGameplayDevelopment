using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleSlider : MonoBehaviour
{
    public float timeScale = 1f;

    public void Update()
    {
        Time.timeScale = timeScale;   
    }
}
