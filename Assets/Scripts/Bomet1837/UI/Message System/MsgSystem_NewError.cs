using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Error Message", menuName = "Message System/New/Error Message")]
public class MsgSystem_NewError : MsgSystem
{
    public void Awake()
    {
        type = MsgType.Error;
        
    }
}