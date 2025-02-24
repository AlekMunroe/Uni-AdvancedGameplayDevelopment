using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOB : MonoBehaviour
{
    private TTCCinemachineVariant TTCCV; 
    private UIFunctions UIF;
    private Vector3 origin;
    void Start()
    {
        TTCCV = FindObjectOfType<TTCCinemachineVariant>();
        UIF = FindObjectOfType<UIFunctions>();
        origin = this.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OOB"))
        {
            if (gameObject.CompareTag("Player"))
            {
                UIF.OOB(this.gameObject, origin);
                TTCCV.ResetCamera();
            }
            else
            {
                UIF.OOB(this.gameObject, origin);
            }
        }

        
        
    }

    public void OOBButton()
    {
        UIF.OOB(gameObject, origin);
        TTCCV.ResetCamera();
    }
}
