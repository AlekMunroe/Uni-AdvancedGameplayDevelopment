using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Warning Message", menuName = "Message System/New/Warning Message")]
public class MsgSystem_NewWarning : MsgSystem
{
    public void Awake()
    {
        type = MsgType.Warning;
    }
}  