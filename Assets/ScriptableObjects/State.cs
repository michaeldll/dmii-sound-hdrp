using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "ScriptableObjects/State", order = 2)]
public class State : ScriptableObject
{
    private bool _state = false;

    public bool GetState
    {
        get { return _state; }
    }

    public void SetState(bool newState)
    {
        _state = newState;
    }
}
