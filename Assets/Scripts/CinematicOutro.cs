using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Playables;

public class CinematicOutro : MonoBehaviour
{
    [SerializeField]
    private float _colorExposure;

    [SerializeField]
    private Volume _volume;

    [SerializeField]
    private PlayableDirector _director;

    private ColorAdjustments _colorAdjustments;

    public void FadeOut()
    {
        _director.Play();
    }

    // Hook
    void Awake()
    {
        _volume.profile.TryGet(out _colorAdjustments);
        _colorAdjustments.postExposure.value = 0f;
    }

    void Start()
    {
        FadeOut();
    }

    void Update() {
        _colorAdjustments.postExposure.value = _colorExposure;
    }

    void OnDrawGizmos() {
        if (_colorAdjustments)
        {
            _colorAdjustments.postExposure.value = _colorExposure;
        }
    }
}
