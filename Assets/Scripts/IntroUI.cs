using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroUI : MonoBehaviour
{
    private VideoPlayerUI _videoPlayerUI;
    private PlayableDirector _timeline;

    [SerializeField]
    private double _interactionTime;

    [SerializeField]
    private bool _stopAtInteraction = true;

    private bool _isInputSelected = false;
    private bool _isComplete = false;

    // Public
    public void Play()
    {
        _videoPlayerUI.Play();
    }

    public void TransitionOut()
    {
        _timeline.Play();
    }

    // Private
    private void OnCompleteHandler()
    {
        TransitionOut();
    }

    // Hooks
    void Awake()
    {
        _videoPlayerUI = GetComponentInChildren<VideoPlayerUI>();
        _timeline = GetComponentInChildren<PlayableDirector>();
    }

    void Update()
    {
        if (_stopAtInteraction && _videoPlayerUI.isPlaying && !_isInputSelected && _videoPlayerUI.currentTime >= _interactionTime)
        {
            _videoPlayerUI.Pause();
        }

        if (_videoPlayerUI.isPaused && _isInputSelected)
        {
            _videoPlayerUI.Play();
        }

        if (!_isComplete && _videoPlayerUI.isComplete)
        {
            _isComplete = true;
            OnCompleteHandler();
        }
    }
}
