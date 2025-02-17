using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Success Message", menuName = "Message System/New/Success Message")]
public class MsgSystem_NewSuccess : MsgSystem
{
    public void Awake()
    {
        type = MsgType.Success;
    }
}
