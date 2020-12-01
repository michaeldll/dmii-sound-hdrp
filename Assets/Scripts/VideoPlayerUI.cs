using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerUI : MonoBehaviour
{
    public double currentTime = 0d;

    [HideInInspector]
    public bool isPlaying = false;

    [HideInInspector]
    public bool isPaused = false;

    [HideInInspector]
    public bool isComplete = false;

    private Canvas _canvasUI;
    private VideoPlayer _videoPlayer;

    // Public
    public void Play()
    {
        _videoPlayer.Play();
    }

    public void Pause()
    {
        _videoPlayer.Pause();
    }

    // Private
    void IsCompleteHandler(UnityEngine.Video.VideoPlayer vp)
    {
        isComplete = true;
    }

    void Awake()
    {
        _canvasUI = GetComponentInChildren<Canvas>();
        _videoPlayer = GetComponentInChildren<VideoPlayer>();

        _videoPlayer.loopPointReached += IsCompleteHandler;
    }

    void Update()
    {
        currentTime = _videoPlayer.time;
        isPlaying = _videoPlayer.isPlaying;
        isPaused = _videoPlayer.isPaused;
    }
}
