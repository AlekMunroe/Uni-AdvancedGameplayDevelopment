using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrompts : MonoBehaviour
{
    public TMPro.TextMeshProUGUI promptText;
    public GameObject promptUI;
    public MsgSystem promptMsg;
    
    private UIFunctions UIFunctions;
    private DBInteraction DBInteraction;
    
    void Start()
    {
        UIFunctions = GameObject.FindObjectOfType<UIFunctions>();
        DBInteraction = GameObject.FindObjectOfType<DBInteraction>();
        
        UIFunctions.CloseUI(promptUI);
    }
    void Update()
    {
        if (DBInteraction.isInteractable && !DBInteraction.dBoxUI.activeSelf)
        {
            UIFunctions.OpenUI(promptUI);
            var placeholders = new Dictionary<string, string>
            {
                { "[InteractKey]", "E" }
            };
            promptText.text = promptMsg.GetMsg(placeholders);
        }
        else
        {
            UIFunctions.CloseUI(promptUI);
        }
    }
}
