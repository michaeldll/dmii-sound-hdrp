using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Playables;

public class CinematicControllerOutro : MonoBehaviour
{
    [HideInInspector]
    public double duration;

    [SerializeField]
    private float _colorExposure;

    [SerializeField]
    private Volume _volume;

    [SerializeField]
    private PlayableDirector _fadeOutDirector;

    [SerializeField]
    private PlayableDirector _fadeInDirector;

    private ColorAdjustments _colorAdjustments;

    public void PlayCinematic()
    {
        if (!_fadeOutDirector) return;
        _fadeOutDirector.Play();
    }

    public void Reset()
    {
        if (!_fadeInDirector) return;
        _fadeInDirector.Play();
    }

    // Hook
    void Awake()
    {
        if (_volume)
        {
            _volume.profile.TryGet(out _colorAdjustments);
            _colorAdjustments.postExposure.value = 0f;
        }

        duration = _fadeOutDirector ? _fadeOutDirector.duration : 0f;
    }

    void Update()
    {
        if (!_colorAdjustments) return;
        _colorAdjustments.postExposure.value = _colorExposure;
    }
}
