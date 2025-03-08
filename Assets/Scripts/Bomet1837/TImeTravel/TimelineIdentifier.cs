using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimelineIdentifier : MonoBehaviour
{
    private GameObject player;
    public int currentTimeline = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("dimensionMain"))
        {
            currentTimeline = 1;
        }
        else if(other.CompareTag("dimensionFuture"))
        {
            currentTimeline = 2;
        }
    }
    
}
