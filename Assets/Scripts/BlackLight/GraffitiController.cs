using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GraffitiController : MonoBehaviour
{
    private GameObject _blackLight;

    void Update()
    {
        if (_blackLight != null)
        {
            //Force the graffiti off if the blacklight is no longer enabled in the hierarchy
            if (!_blackLight.activeInHierarchy)
            {
                this.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlackLight")
        {
            _blackLight = other.gameObject;
            this.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BlackLight")
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
