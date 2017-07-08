using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerInput : CharacterInput {

    private x360_Gamepad gamepad;
    //private WraithController controller;

    void OnEnable()
    {
        if (playerIndex == 0) return;
        gamepad = GamepadManager.Instance.GetGamepad(playerIndex);
        //controller = GetComponent<WraithController>();
        //controller.OnRumbleEvent += rumbleEvent;
    }

    void Update () {
        if (gamepad.IsConnected)
        {
            if (gamepad.GetButtonDown("A"))
            {
                //controller.CommandDash();
            }

            //if (gamepad.GetButton("X"))
            //{
            //    controller.CommandSetBoost(true);
            //}
            //else
            //{
            //    controller.CommandSetBoost(false);
            //}

            //if (gamepad.GetStick_L().X != 0f || gamepad.GetStick_L().Y != 0f)
            //{
            //    controller.CommandSetDirection(new Vector2(gamepad.GetStick_L().X, gamepad.GetStick_L().Y));
            //}
        }
    }

    void rumbleEvent(float timer, float strength, float fadeTime)
    {
        gamepad.AddRumble(timer, new Vector2(strength, strength), fadeTime);
    }
}
