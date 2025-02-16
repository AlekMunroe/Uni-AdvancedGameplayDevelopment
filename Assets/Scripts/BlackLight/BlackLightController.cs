using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackLightController : MonoBehaviour
{
    public static BlackLightController instance;

    private bool _hasLight;

    public GameObject blackLight;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && _hasLight)
        {
            blackLight.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            blackLight.SetActive(false);
        }
    }

    public void HasLight(bool hasPickedUpBlackLight)
    {
        _hasLight = hasPickedUpBlackLight;
    }
}
