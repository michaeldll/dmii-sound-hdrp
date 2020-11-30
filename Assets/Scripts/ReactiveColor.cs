using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveColor : MonoBehaviour
{
    [SerializeField]
    Light targetLight;

    [SerializeField]
    Color fadeIn = Color.black;

    [SerializeField]
    Color fadeOut = Color.white;

    [SerializeField]
    DataObject data;

    [SerializeField]
    EasingFunction.Ease colorsEase = EasingFunction.Ease.EaseInOutQuart;

    [SerializeField]
    float amplitude = 1;

    [SerializeField]
    float targetLightIntensity = 14.5f;

    [SerializeField]
    bool flicker = true;

    [SerializeField]
    Type type = Type.Mesh;
    public enum Type
    {
        Mesh,
        Light
    }

    [SerializeField]
    float flickerAmplitude = 1f;

    [SerializeField]
    float flickerFrequency = 1f;

    [SerializeField]
    float flickerIntensity = 0.1f;

    float value;
    float funcedValue;

    Renderer _targetRenderer;
    EasingFunction.Function _func;

    void Start()
    {
        _func = EasingFunction.GetEasingFunction(colorsEase);
        _targetRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    void FadeLight()
    {
        //get easing function value
        value = Mathf.Clamp(data.micVolumeNormalized * amplitude, 0, 1);
        funcedValue = _func(0, 1, value); ;

        //lower base value with a sinus to get flickering light effect
        if (flicker)
        {
            float sin = Mathf.Sin(Time.frameCount * flickerFrequency) * flickerAmplitude;
            funcedValue -= flickerIntensity * sin;
        }

        if (type == Type.Light)
        {
            targetLight.intensity = Mathf.Clamp(funcedValue * targetLightIntensity, 0, targetLightIntensity);
            targetLight.color = Color.Lerp(fadeOut, fadeIn, Mathf.Clamp(funcedValue, 0, 1));
        }

        if (type == Type.Mesh)
        {
            _targetRenderer.material.SetColor("_EmissionColor", Color.Lerp(fadeOut, fadeIn, Mathf.Clamp(funcedValue, 0, 1)));
            _targetRenderer.material.SetColor("_BaseColor", Color.Lerp(fadeOut, fadeIn, Mathf.Clamp(funcedValue, 0, 1)));
        }
    }

    void Update()
    {
        FadeLight();
    }
}
