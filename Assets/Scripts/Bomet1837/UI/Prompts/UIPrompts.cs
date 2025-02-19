using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPrompts : MonoBehaviour
{
    public TMPro.TextMeshProUGUI promptText;
    public GameObject promptUI;
    public MsgSystem promptMsg;
    
    private UIFunctions UIFunctions;
    [SerializeField] private DBInteraction[] DBInteraction;
    [SerializeField] private bool[] isInteractable;
    
    void Start()
    {
        UIFunctions = FindObjectOfType<UIFunctions>();
        DBInteraction = FindObjectsOfType<DBInteraction>();
        isInteractable = new bool[DBInteraction.Length];
        
        UIFunctions.CloseUI(promptUI);
    }
    void Update()
    {
        for (var i = 0; i < DBInteraction.Length; i++)
        {
            isInteractable[i] = DBInteraction[i].isInteractable;
        }

        for (var i = 0; i < isInteractable.Length; i++)
        {
            if (isInteractable[i])
            {
                promptUI.SetActive(true);
                var placeholders = new Dictionary<string, string>
                {
                    { "[InteractKey]", "E" }
                };
                promptText.text = promptMsg.GetMsg(placeholders);
            }
            else if (!isInteractable.Contains(true))
            {
                promptUI.SetActive(false);
            }
        }




    }
    
    void CheckInteractable(DBInteraction dbInteraction)
    {
        
    }
}
