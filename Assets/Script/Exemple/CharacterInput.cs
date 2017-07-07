using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public abstract class CharacterInput : MonoBehaviour {
    public int playerIndex;

    protected WraithController controller;

    void Start()
    {
        controller = GetComponent<WraithController>();
    }
}
