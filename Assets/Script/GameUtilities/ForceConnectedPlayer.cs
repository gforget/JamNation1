using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceConnectedPlayer : MonoBehaviour {

    private void Awake()
    {
        GlobalVariables.m_PlayerActives[0] = true;
        GlobalVariables.m_PlayerActives[1] = true;
        GlobalVariables.m_PlayerActives[2] = true;
        GlobalVariables.m_PlayerActives[3] = true;
    }
}
