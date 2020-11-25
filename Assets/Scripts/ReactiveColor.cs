using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveColor : MonoBehaviour
{
    [Tooltip("Target Renderer Material")]
    [SerializeField]
    Renderer targetRenderer;

    [Tooltip("Target Light")]
    [SerializeField]
    Light targetLight;

    [Tooltip("Fade In Color")]
    [SerializeField]
    Color fadeIn = Color.black;

    [SerializeField]
    [Tooltip("Fade Out Color")]
    Color fadeOut = Color.white;

    [SerializeField]
    DataObject data;

    [SerializeField]
    EasingFunction.Ease colorsEase = EasingFunction.Ease.EaseInOutQuart;

    EasingFunction.Function _func;

    [Tooltip("Multiply mic volume by this")]
    [SerializeField]
    float amplitude = 1;

    [Tooltip("Current light value")]
    [SerializeField]
    float value;

    [SerializeField]
    float funcedValue;

    [SerializeField]
    float targetLightIntensity = 14.5f;

    void Start()
    {
        _func = EasingFunction.GetEasingFunction(colorsEase);
    }

    void FadeLight()
    {

        value = Mathf.Clamp(data.micVolumeNormalized * amplitude, 0, 1);
        funcedValue = _func(0, 1, value); ;

        targetLight.intensity = Mathf.Clamp(funcedValue * targetLightIntensity, 0, targetLightIntensity);
        targetLight.color = Color.Lerp(fadeOut, fadeIn, Mathf.Clamp(funcedValue, 0, 1));
        targetRenderer.material.SetColor("_EmissionColor", Color.Lerp(fadeOut, fadeIn, Mathf.Clamp(funcedValue, 0, 1)));
        targetRenderer.material.SetColor("_BaseColor", Color.Lerp(fadeOut, fadeIn, Mathf.Clamp(funcedValue, 0, 1)));
    }

    void Update()
    {
        FadeLight();
    }
}
