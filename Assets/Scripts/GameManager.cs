﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isDebug = false;

    [SerializeField]
    private Navigation _worldsNavigation = null;

    [SerializeField]
    private State _readyState = null;

    [SerializeField]
    private State _gameOverState = null;

    [SerializeField]
    private IntroUI _introUI;

    [SerializeField]
    private CinematicControllerIntro _cinematicControllerIntro;

    [SerializeField]
    private CinematicControllerOutro _cinematicControllerOutro;

    [SerializeField]
    private World[] _worlds = null;

    [SerializeField]
    private bool _shuffleOrder = false;

    [SerializeField]
    private PlayerMovement _playerMovement = null;

    [SerializeField]
    private VoidEvent onInterfaceComplete;

    [SerializeField]
    private bool gameStartOnAwake = true;

    [SerializeField]
    private DataObject data = null;

    private delegate void TimeoutCallback();

    // Private
    private void InitNavigation()
    {
        _worldsNavigation.Reset();
        _readyState.SetState(false);

        SetNavigationOrder();

        if (_shuffleOrder)
        {
            ShuffleNavigationOrder();
        }

        _worldsNavigation.InitNavigation();
        _playerMovement.InitPosition(_worlds[_worldsNavigation.active].transform.position);
    }

    private void SetNavigationOrder()
    {
        List<World> worlds = new List<World>(_worlds);
        List<int> order = new List<int>();

        for (int i = 0; i < worlds.Count; i++)
        {
            order.Add(i);
        }

        _worldsNavigation.SetOrder(order.ToArray());
    }

    private void ShuffleNavigationOrder()
    {
        List<int> order = new List<int>(_worldsNavigation.order);
        List<int> newOrder = new List<int>();

        // Starting world is always 0
        newOrder.Add(0);

        int initialLength = order.Count;

        for (int i = 1; i < initialLength; i++)
        {
            int randomIndex = (int)Mathf.Round(UnityEngine.Random.Range(1f, (float)order.Count - 1));
            newOrder.Add(order[randomIndex]);
            order.RemoveAt(randomIndex);
        }

        _worldsNavigation.SetOrder(newOrder.ToArray());
    }

    // Intro
    private void PlayCinematicIntro()
    {
        if (_isDebug) return;

        _cinematicControllerIntro.PlayCinematic();
        float duration = (float)_cinematicControllerIntro.duration;
        TimeoutCallback onCompleteCallback = IntroCompletedHandler;
        StartCoroutine(SetTimeout(duration, IntroCompletedHandler));
    }

    // Outro
    private void PlayCinematicOutro()
    {
        _cinematicControllerOutro.PlayCinematic();
        float duration = (float)_cinematicControllerOutro.duration;
        TimeoutCallback onCompleteCallback = OutroCompletedHandler;
        StartCoroutine(SetTimeout(duration, OutroCompletedHandler));
    }

    private void CheckVolume()
    {
        if (_isDebug)
        {
            TimeoutCallback onStopPlayingCallback = onStopPlayingHandler;
            StartCoroutine(Debounced(2f, onStopPlayingCallback));
        }
        // Trigger GameOver when user stopped playing music
        else if (data.micVolumeNormalized > 0.1f)
        {
            TimeoutCallback onStopPlayingCallback = onStopPlayingHandler;
            StartCoroutine(Debounced(2f, onStopPlayingCallback));
        }
    }

    private void SpeedUpIntro()
    {
        VideoPlayer videoPlayer = _introUI.GetComponentInChildren<VideoPlayer>();
        videoPlayer.playbackSpeed = 10;

        _cinematicControllerIntro.End();
        IntroCompletedHandler();
    }

    // Handlers
    private void IntroUICompletedHandler()
    {
        PlayCinematicIntro();
    }

    private void IntroCompletedHandler()
    {
        _worlds[_worldsNavigation.active].Enter();
        _readyState.SetState(true);
        _cinematicControllerIntro.Reset();
    }

    private void OutroCompletedHandler()
    {
        InitNavigation();
        _worlds[_worldsNavigation.active].Enter();
        _readyState.SetState(true);
        _cinematicControllerOutro.Reset();
    }

    private void onStopPlayingHandler()
    {
        _gameOverState.SetState(true);
        PlayCinematicOutro();
    }

    // Hooks
    void Awake()
    {
        _readyState.SetState(gameStartOnAwake);
        data.SetVolume(0f);
    }

    void Start()
    {
        InitNavigation();

        // Play intro with Audio Input Selection
        _introUI.Play();

        // When Intro finished Play Cinematic
        onInterfaceComplete.e.AddListener(IntroUICompletedHandler);

        if (_isDebug)
        {
            SpeedUpIntro();
        }
    }

    void Update()
    {
        if (_worldsNavigation.IsGameOverAllowed) CheckVolume();
    }

    // Utils
    IEnumerator SetTimeout(float time, TimeoutCallback Method)
    {
        yield return new WaitForSeconds(time);

        Method();
    }

    private Guid _latest;

    IEnumerator Debounced(float time, TimeoutCallback Method)
    {
        // generate a new id and set it as the latest one 
        Guid guid = Guid.NewGuid();
        _latest = guid;

        // set the denounce duration here
        yield return new WaitForSeconds(time);

        // check if this call is still the latest one
        if (_latest == guid)
        {
            Method();
        }
    }
}
