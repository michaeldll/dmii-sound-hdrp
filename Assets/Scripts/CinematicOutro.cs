using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class CinematicOutro : MonoBehaviour
{
    [SerializeField]
    private Volume _volume;
    private ColorAdjustments _colorAdjustments;

    private void Awake()
    {
        _volume.profile.TryGet(out _colorAdjustments);
        // _colorAdjustments.postExposure.value = -10f;
    }
}
