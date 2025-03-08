using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Message", menuName = "Message System/New/Default Message")]

public class MsgSystem_NewMessage : MsgSystem
{
    public void Awake()
    {
        type = MsgType.Default;
    }
}