using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayerControls;

public class BoxInteractPrompt : MonoBehaviour
{
    private TextMeshProUGUI promptText;
    private PushableBlock pushableBlock;
    public int drawRays = 32;
    public float radius = 1.5f;

    [SerializeField] private float fontMin = 14f;
    [SerializeField] private float fontMax = 36f;

    private void Start()
    {
        pushableBlock = GetComponent<PushableBlock>();
        if (pushableBlock == null)
        {
            Debug.LogError("PushableBlock component not found in the scene.");
        }
        promptText = GetComponentInChildren<TextMeshProUGUI>();
        if (promptText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found in the scene.");
        }

        promptText.text = "";
    }

    private void FixedUpdate()
    {
        RaycastArea();
    }

    private void RaycastArea()
    {
        bool playerInside = false;

        for (var i = 0; i < drawRays; i++)
        {
            var angle = i * (360 / drawRays);
            Vector3 dir = Quaternion.Euler(0, angle, 0) * transform.forward;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, radius))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    float dist = Vector3.Distance(transform.position, hit.point);
                    float fontSize = Mathf.Lerp(fontMax, fontMin, dist / radius);
                    promptText.fontSize = fontSize;

                    playerInside = true;
                    break;
                }
            }
        }

        switch (playerInside)
        {
            case true:
                if(!pushableBlock.canMoveX && !pushableBlock.canMoveZ)
                {
                    promptText.text = "[" + PlayerControls.PlayerControls.isDisabledMiniTip + "]";
                }
                else
                {
                    promptText.text = "[" + PlayerControls.PlayerControls.pushKey.ToString() + "]";
                }
                break;

            case false:
                break;
        }

        if (playerInside)
        {
            promptText.text = "[" + PlayerControls.PlayerControls.pushKey.ToString() + "]";
        }
        else
        {
            promptText.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }


    private void OnDrawGizmos()
    {
        bool isInsideDebug = false; 


        Gizmos.color = Color.red;
        for (var i = 0; i < drawRays; i++)
        {
            var angle = i * (360 / drawRays);
            Vector3 dir = Quaternion.Euler(0, angle, 0) * transform.forward;
            Gizmos.DrawRay(transform.position, dir * radius);
            
            RaycastHit hit;
            if(Physics.Raycast(transform.position, dir, out hit, radius))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isInsideDebug = true;
                }
                else
                {
                    isInsideDebug = false;
                }
            }

            if (isInsideDebug)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

        }
    }


    



}
