using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script will handle the change in cameras
/// </summary>
public class TimeTravelController : MonoBehaviour
{
    public static TimeTravelController Instance;
    
    [SerializeField] private GameObject fadeUI;
    private GameObject playerObject;
    private bool isInPast = true;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void TimeTravel(Camera oldCamera, Camera newCamera, float travelLocation)
    {
        StartCoroutine(TransitionUI(oldCamera, newCamera, travelLocation));
    }

    IEnumerator TransitionUI(Camera oldCamera, Camera newCamera, float travelLocation)
    {
        //Fade in
        fadeUI.SetActive(true);
        fadeUI.GetComponent<Animation>().Play("FadeInAnim");
        
        yield return new WaitForSeconds(0.4f);
        
        //Change the cameras
        newCamera.gameObject.SetActive(true);
        oldCamera.gameObject.SetActive(false);
        
        //Move the player (+travelLocation on the x)
        playerObject.transform.position = new Vector3(playerObject.transform.position.x + travelLocation,
            playerObject.transform.position.y + 0.1f, playerObject.transform.position.z);

        //Fade out
        fadeUI.GetComponent<Animation>().Play("FadeOutAnim");
        yield return new WaitForSeconds(0.4f);
        fadeUI.SetActive(false);
    }
}
