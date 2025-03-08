using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum MsgType
{
    Default,
    Error,
    Warning,
    Info,
    Success,
}

public class MsgSystem : ScriptableObject
{
    [TextArea(20, 20)] public string message;
    public MsgType type;
    private static int IDCounter = 0;
    private bool _isDestroyed = false;

    public MsgSystem(string message, MsgType type)
    {
        IDCounter++;
        this.message = $"{message} (ID: {IDCounter})";

        this.type = type;
        _isDestroyed = false;
    }

    public static MsgSystem CreateInstance(string message, MsgType type)
    {
        return new MsgSystem(message, type);
    }

    public void Remove(List<MsgSystem> messages)
    {
        if (!_isDestroyed && messages.Contains(this))
        {
            messages.Remove(this);
            Debug.Log($"Message with ID: {IDCounter} has been destroyed");
            _isDestroyed = true;
        }
    }
    
    public string GetMsg(Dictionary<string, string> placeholders)
    {
       string formattedMessage = message;
       foreach (var placeholder in placeholders)
       {
           formattedMessage = formattedMessage.Replace(placeholder.Key, placeholder.Value);
       }

       return formattedMessage;
    }

    protected MsgSystem() {}
}









