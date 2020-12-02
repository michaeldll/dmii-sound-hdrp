using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroUI : MonoBehaviour
{
    private VideoPlayerUI _videoPlayerUI;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private double _interactionTime;

    [SerializeField]
    private bool _stopAtInteraction = true;

    [SerializeField]
    private GameObject _buttonPanel;

    private bool _isInputSelected = false;
    private bool _isComplete = false;

    [SerializeField]
    private VoidEvent onInterfaceComplete;

    // Public
    public void Play()
    {
        _videoPlayerUI.Play();
    }

    public void TransitionOut()
    {
        _animator.SetBool("isPlaying", true);
    }

    public void SetInputSelected(bool toggle)
    {
        _isInputSelected = toggle;
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

        onInterfaceComplete.e.AddListener(OnCompleteHandler);
    }

    void Update()
    {
        if (_stopAtInteraction && _videoPlayerUI.isPlaying && !_isInputSelected && _videoPlayerUI.currentTime >= _interactionTime)
        {
            _videoPlayerUI.Pause();
            _buttonPanel.SetActive(true);
            _stopAtInteraction = false;
        }

        if (_videoPlayerUI.isPaused && _isInputSelected)
        {
            _videoPlayerUI.Play();
            _isInputSelected = false;
        }

        if (!_isComplete && _videoPlayerUI.isComplete)
        {
            _isComplete = true;
            onInterfaceComplete.e.Invoke();
        }
    }
}
