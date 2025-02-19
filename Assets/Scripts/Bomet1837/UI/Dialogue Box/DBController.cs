using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DBController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dialogueText;
    public GameObject dBoxUI;
    
    private UIFunctions UIFunctions;
    public int dialogueIndex = 0;
    public List<MsgSystem> dialogues;

    void Start()
    {
        UIFunctions = GameObject.FindObjectOfType<UIFunctions>();
        ClearDialogue();
        HideBox();
    }

    void Update()
    {
        if (dialogueIndex >= 0 && dialogueIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[dialogueIndex].message;
        }
    }


    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }
    
    public void ClearDialogue()
    {
        dialogues.Clear();
        dialogueIndex = 0;
    }
    
    public void NextDialogue()
    {
        if (dialogueIndex < dialogues.Count)
        {
            SetDialogue(dialogues[dialogueIndex].message);
            dialogueIndex++;
        }
        else if (dialogueIndex == dialogues.Count)
        {
            ClearDialogue();
            HideBox();
        }
    }
    
    public void ShowBox()
    {
        UIFunctions.DisableControls();
        UIFunctions.OpenUI(gameObject);
    }
    
    public void HideBox()
    {
        UIFunctions.EnableControls();
        UIFunctions.CloseUI(gameObject);
    }
    
    public void AddDialogue(MsgSystem dialogue)
    {
        dialogues.Add(dialogue);
        
    }
    
    
    
    
}
