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
        _fadeOutDirector.Play();
    }

    public void Reset()
    {
        _fadeInDirector.Play();
    }

    // Hook
    void Awake()
    {
        _volume.profile.TryGet(out _colorAdjustments);
        _colorAdjustments.postExposure.value = 0f;

        duration = _fadeOutDirector.duration;
    }

    void Update()
    {
        _colorAdjustments.postExposure.value = _colorExposure;
    }
}
