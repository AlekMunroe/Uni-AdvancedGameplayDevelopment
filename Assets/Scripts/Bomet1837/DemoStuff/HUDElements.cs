using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

/// <summary>
/// A class that manages various for the game's HUD.
/// </summary>

public class HUDElements : MonoBehaviour
{
    public GameObject HUD;
    public TimelineIdentifier ti;
    public TMP_Text locationDate, level, objective, messageText;
    public MsgSystem ldNode, ldNode2, levelNode, levelNode2, objNode;
    public List<MsgSystem> messages;
    
    
    [SerializeField] private UIFunctions _uiFunctions;
    
    //lol
        private KeyItem [] keyItems; 
        private ContainerController [] containers; 
        private DoorController [] doors;
        private ComboLockController [] comboLocks;
        private BlackLight_Pickup [] blacklights;
        
        private object [][] interactables;
        
        

    void Start()
    {
        _uiFunctions = GetComponent<UIFunctions>();
        messages = new List<MsgSystem>();
        
        keyItems = FindObjectsOfType<KeyItem>();
        containers = FindObjectsOfType<ContainerController>();
        doors = FindObjectsOfType<DoorController>();
        comboLocks = FindObjectsOfType<ComboLockController>();
        blacklights = FindObjectsOfType<BlackLight_Pickup>();
        interactables = new object[][] {keyItems, containers, doors, comboLocks, blacklights} ;
        
    }
    

    void Update()
    {
        if (ti.currentTimeline == 2)
        {
            locationDate.text = ldNode2.message;
            level.text = levelNode2.message;
        }
        else
        {
            locationDate.text = ldNode.message;
            level.text = levelNode.message;
        }
        
        objective.text = objNode.message;
        
        if (LevelSequencer.Instance.isComplete)
        {
            _uiFunctions.CloseUI(HUD);
        }
        
        
        UpdateMessages();
        InteractableMessageUpdates();
    }

    void InteractableMessageUpdates()
    {
        foreach (var interactable in interactables)
        {
            foreach (var i in interactable)
            {
                switch (i)
                {
                    case KeyItem item:
                    {
                        KeyItem keyItem = item;
                        if (keyItem == null && !keyItem.messageDisplayed)
                        {
                            messages.Add(MsgSystem.CreateInstance("Picked up " + keyItem.keyName, MsgType.Success));
                            keyItem.messageDisplayed = true;

                            
                        }

                        break;
                    }
                    case ContainerController controller:
                    {
                        ContainerController container = controller;
                        if (container == null && !container.messageDisplayed)
                        {
                            messages.Add(MsgSystem.CreateInstance("Opened " + container.contName, MsgType.Success));
                            container.messageDisplayed = true;

                        }
                        else if (container.wasItLocked && !container.messageDisplayed)
                        {
                            messages.Add(MsgSystem.CreateInstance(container.contName + "is locked, you still need the key.", MsgType.Success));
                            container.messageDisplayed = true;
                        }
                        else if (container.wasItKeyless == true && !container.messageDisplayed)
                        {
                            messages.Add(MsgSystem.CreateInstance("Unlocked " + container.contName + ". Didn't need a key!", MsgType.Success));
                            container.messageDisplayed = true;
                        }

                        break;
                    }
                    case DoorController controller:
                    {
                        DoorController door = controller;
                        if (door.isDoorLocked == false && !door.messageDisplayed)
                        {
                            messages.Add(MsgSystem.CreateInstance("Unlocked Door ", MsgType.Success));
                            door.messageDisplayed = true;

                        }
                        else if (door.wasItLocked && !door.messageDisplayed)
                        {
                            messages.Add(MsgSystem.CreateInstance("Door is locked, you still need the key.", MsgType.Success));
                            door.messageDisplayed = true;
                        }

                        break;
                    }
                    case ComboLockController controller:
                    {
                        ComboLockController comboLock = controller;
                        if (comboLock.isUnlocked && !comboLock.messageDisplayed)
                        {
                            messages.Add(MsgSystem.CreateInstance("Unlocked " + comboLock.gameObject.name, MsgType.Success));
                            comboLock.messageDisplayed = true;
                        }

                        break;
                    }
                    case BlackLight_Pickup pickup:
                    {
                        BlackLight_Pickup blacklight = pickup;
                        if (blacklight == null && !blacklight.messageDisplayed)
                        {
                           messages.Add( MsgSystem.CreateInstance("Picked up " + blacklight.gameObject.name, MsgType.Success));
                           blacklight.messageDisplayed = true;
                        }

                        break;
                    }
                }
            }
        }
    }

    void UpdateMessages()
    {
        messageText.text = string.Join("\n", messages.ConvertAll(m => m.message));

        if (messages.Count > 0)
        {
            for(int i = messages.Count - 1; i >= 0; i--)
            {
                StartCoroutine(RemoveMessage(messages[i], 5f));
            }
        }

        foreach (var message in messages)
        {
            switch (message.type)
            {
                case MsgType.Default:
                    messageText.color = Color.white;
                    break;
                case MsgType.Error:
                    messageText.color = Color.red;
                    break;
                case MsgType.Warning:
                    messageText.color = Color.yellow;
                    break;
                case MsgType.Info:
                    messageText.color = Color.blue;
                    break;
                case MsgType.Success:
                    messageText.color = Color.green;
                    break;
            }
        
        }
    }
    
    IEnumerator RemoveMessage(MsgSystem message, float delay)
    {
        yield return new WaitForSeconds(delay);
        message.Remove(messages);
    }
}