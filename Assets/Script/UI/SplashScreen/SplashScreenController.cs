using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenController : MonoBehaviour {

    public Color m_SelectedColor = Color.yellow;
    public Color m_ActiveColor = Color.white;
    public Color m_InactiveColor = Color.gray;

    [SerializeField]
    private ButtonMainMenu[] ButtonMainMenuArray;

    [SerializeField]
    private Text AlertNbOfController;

    public static SplashScreenController Instance
    {
        get
        {
            return m_Instance ?? (m_Instance = GameObject.FindObjectOfType(typeof(SplashScreenController)) as SplashScreenController);
        }
    }
    private static SplashScreenController m_Instance;

    private int m_IndexButtonSelected
    {
        get
        {
            return _m_IndexButtonSelected;
        }

        set
        {
            _m_IndexButtonSelected = Mathf.Clamp(value, 0, GamepadManager.Instance.ConnectedTotal()-1);
        }
    }

    private int _m_IndexButtonSelected = 0;
    
    void Start()
    {
        ButtonMainMenuArray[m_IndexButtonSelected].CurrentButtonState = ButtonMainMenu.ButtonState.Selected;
    }

    void Update ()
    {
        if (GamepadManager.Instance.ConnectedTotal() == 0)
        {
            AlertNbOfController.gameObject.SetActive(true);
        }
        else
        {
            AlertNbOfController.gameObject.SetActive(false);
        }

        if (GamepadManager.Instance.GetButtonDownAny("A"))
        {
            int nbOfConnectorSent = 0;

            GlobalVariables.m_PlayerActives[0] = false;
            GlobalVariables.m_PlayerActives[1] = false;
            GlobalVariables.m_PlayerActives[2] = false;
            GlobalVariables.m_PlayerActives[3] = false;

            for (int i=1; i<=4; i++)
            {
                if (GamepadManager.Instance.GetGamepad(i).IsConnected)
                {
                    GlobalVariables.m_PlayerActives[i - 1] = true;
                    nbOfConnectorSent++;
                    if (nbOfConnectorSent == ButtonMainMenuArray[m_IndexButtonSelected].m_NbOfPlayer) break;
                }
            }

            SceneManager.LoadScene("Level_principal", LoadSceneMode.Single);
        }

        if (GamepadManager.Instance.GetAnyStickTapUp_L() || GamepadManager.Instance.GetButtonDownAny("DPad_Up"))
        {
            m_IndexButtonSelected--;
            SelectButton(m_IndexButtonSelected);
        }

        if (GamepadManager.Instance.GetAnyStickTapDown_L() || GamepadManager.Instance.GetButtonDownAny("DPad_Down"))
        {
            m_IndexButtonSelected++;
            SelectButton(m_IndexButtonSelected);
        }
    }

    void SelectButton(int indexSelected)
    {
        foreach (ButtonMainMenu button in ButtonMainMenuArray)
        {
            if (button.CurrentButtonState == ButtonMainMenu.ButtonState.Inactive || button.m_NbOfPlayer == (indexSelected-1)) continue;
            button.CurrentButtonState = ButtonMainMenu.ButtonState.Active;
        }

        ButtonMainMenuArray[indexSelected].CurrentButtonState = ButtonMainMenu.ButtonState.Selected;
    }

}
