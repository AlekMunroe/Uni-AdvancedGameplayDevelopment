using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PlayerControls;

public class UIPrompts : MonoBehaviour
{
    public TMPro.TextMeshProUGUI promptText;
    public GameObject promptUI;
    public MsgSystem promptMsg;
    private UIFunctions UIFunctions;
    [SerializeField] private DBInteraction[] DBInteraction;
   [SerializeField] private bool[] dbIsInteractable;
    [SerializeField] private ComboLockInteraction[] comboLockInteractions;
    [SerializeField] private bool[] comboLockIsInteractable;

    private bool promptActive = false;

    
    void Start()
    {
        UIFunctions = FindObjectOfType<UIFunctions>();

        DBInteraction = FindObjectsOfType<DBInteraction>();
        dbIsInteractable = new bool[DBInteraction.Length];

        comboLockInteractions = FindObjectsOfType<ComboLockInteraction>();
        comboLockIsInteractable = new bool[comboLockInteractions.Length];

        UIFunctions.CloseUI(promptUI);
    }
    void Update()
    {
        promptActive = false;
        CheckDBInteractable(DBInteraction);
        CheckLockInteractable(comboLockInteractions);

        if (!promptActive)
        {
            UIFunctions.CloseUI(promptUI);
        }
    }
    
    void CheckDBInteractable(DBInteraction[] dbInteraction)
    {
        for (var i = 0; i < DBInteraction.Length; i++)
        {
            dbIsInteractable[i] = DBInteraction[i].isInteractable;
        }

        for (var i = 0; i < dbIsInteractable.Length; i++)
        {
            if (dbIsInteractable[i])
            {
                promptUI.SetActive(true);
                var placeholders = new Dictionary<string, string>
                {
                    { "[InteractKey]", PlayerControls.PlayerControls.interactKey.ToString() /*"E"*/ }
                };
                promptText.text = promptMsg.GetMsg(placeholders);
                promptActive = true;
                break;
            }
        }
    }

    void CheckLockInteractable(ComboLockInteraction[] comboLockInteractions)
    {
        for (var i = 0; i < comboLockInteractions.Length; i++)
        {
            comboLockIsInteractable[i] = comboLockInteractions[i].isInteractable;
        }

        for (var i = 0; i < comboLockIsInteractable.Length; i++)
        {
            if (comboLockIsInteractable[i])
            {
                promptUI.SetActive(true);
                var placeholders = new Dictionary<string, string>
                {
                    { "[InteractKey]", PlayerControls.PlayerControls.interactKey.ToString() /*"E"*/ }
                };
                promptText.text = promptMsg.GetMsg(placeholders);
                promptActive = true;
                break;
            }
        }

        


    }
}
