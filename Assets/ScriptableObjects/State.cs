using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "ScriptableObjects/State", order = 2)]
public class State : ScriptableObject
{
    public bool state = false;

    public bool GetState
    {
        get { return state; }
    }

    public void SetState(bool newState)
    {
        state = newState;
    }
}
