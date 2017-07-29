using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMainMenu : MonoBehaviour
{

    public enum ButtonState
    {
        Inactive = 0,
        Active = 1,
        Selected = 2
    }

    public ButtonState CurrentButtonState
    {
        get
        {
            return m_CurrentButtonState;
        }

        set
        {

            m_CurrentButtonState = value;
            switch (m_CurrentButtonState)
            {
                case ButtonState.Inactive:
                    m_ButtonText.color = SplashScreenController.Instance.m_InactiveColor;
                    m_IconSelected.gameObject.SetActive(false);
                    break;
                case ButtonState.Active:
                    m_ButtonText.color = SplashScreenController.Instance.m_ActiveColor;
                    m_IconSelected.gameObject.SetActive(false);
                    break;
                case ButtonState.Selected:
                    m_ButtonText.color = SplashScreenController.Instance.m_SelectedColor;
                    m_IconSelected.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
    
    private ButtonState m_CurrentButtonState = ButtonState.Inactive;

    public int m_NbOfPlayer;

    [SerializeField]
    private Text m_ButtonText;

    [SerializeField]
    private Image m_IconSelected;

    //private x360_Gamepad m_Gamepad;
    private void Awake()
    {
        //m_Gamepad = GamepadManager.Instance.GetGamepad(m_NbOfPlayer);
    }

    private void Update()
    {
        if (GamepadManager.Instance.ConnectedTotal() < m_NbOfPlayer)
        {
            CurrentButtonState = ButtonState.Inactive;
        }
        else
        {
            if (CurrentButtonState != ButtonState.Selected) CurrentButtonState = ButtonState.Active;
        }

        //if (!m_Gamepad.IsConnected)
        //{
        //    CurrentButtonState = ButtonState.Inactive;
        //}
        //else
        //{
        //    if (CurrentButtonState != ButtonState.Selected) CurrentButtonState = ButtonState.Active;
        //}
    }

}
