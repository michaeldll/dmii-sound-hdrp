﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [HideInInspector]
    public Door doorEnter;

    [HideInInspector]
    public Door doorLeave;

    [SerializeField]
    private Navigation _worldsNavigation = null;

    [SerializeField]
    private VirtualPlayer _virtualPlayer = null;

    private int _id;
    private bool _isActive;

    private delegate void TimeoutCallback();

    // Public
    public void Enter()
    {
        UpdateNavigation();

        doorEnter.TransitionIn();
        TimeoutCallback transitionIn = doorLeave.TransitionIn;
        StartCoroutine(SetTimeout(2f, transitionIn));

        // Debug.Log("Enter World : " + _id);
    }

    public void Leave()
    {
        doorLeave.TransitionOut();

        // Debug.Log("Leave World : " + _id);
    }

    private void UpdateNavigation()
    {
        _virtualPlayer.SetActiveWorld(_id);

        doorEnter.SetDestination(_worldsNavigation.previous, -1);
        doorLeave.SetDestination(_worldsNavigation.next, 1);

        _virtualPlayer.SetDestination(_worldsNavigation.next);
    }

    private void CheckState()
    {
        _isActive = _worldsNavigation.active == _id;
    }

    // Hooks
    void Awake() {
        Door[] doors = GetComponentsInChildren<Door>();
        doorEnter = doors[0].name == "Door Enter" ? doors[0] : doors[1];
        doorLeave = doors[1].name == "Door Leave" ? doors[1] : doors[0];   

        string[] nameChars = this.name.Split(char.Parse("_"));
        _id = int.Parse(nameChars[nameChars.Length - 1]);
    }

    // Utils
    IEnumerator SetTimeout(float time, TimeoutCallback Method)
    {
        yield return new WaitForSecondsRealtime(time);

        Method();
    }
}
