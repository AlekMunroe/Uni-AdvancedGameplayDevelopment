using System;
using UnityEngine;

namespace PlayerControls
{
    public class PlayerControls : MonoBehaviour
    {
        // Movement Inputs
        public static KeyCode
            moveForwardKey = KeyCode.W,
            moveBackwardKey = KeyCode.S,
            moveLeftKey = KeyCode.A,
            moveRightKey = KeyCode.D,
            jumpKey = KeyCode.Space,

        // Action Inputs
            interactKey = KeyCode.E,
            pushKey = KeyCode.L,
            blacklightKey = KeyCode.F;

        // UI Resources
        public static string
            isDisabledMiniTip = "X",
            isDisabledFullTip = "(is disabled)",
            isDisabledFullTipColor = "#FF0000";
    }
}