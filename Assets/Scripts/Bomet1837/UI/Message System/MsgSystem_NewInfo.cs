using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Info Message", menuName = "Message System/New/Info Message")]
public class MsgSystem_NewInfo : MsgSystem
{
    public void Awake()
    {
        type = MsgType.Info;
    }
}
