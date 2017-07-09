using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplahScreen : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        if (GamepadManager.Instance.GetButtonAny("A"))
        {
            SceneManager.LoadScene("Level_principal", LoadSceneMode.Single);
        }
	}
}
