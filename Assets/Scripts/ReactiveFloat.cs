using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveFloat : MonoBehaviour
{
    [SerializeField]
    DataObject data;

    [SerializeField]
    float amplitude = 1;

    [SerializeField]
    float floatAmplitude = 1f;

    [SerializeField]
    float floatFrequency = 1f;

    [SerializeField]
    FloatingType type = FloatingType.Sinus;
    public enum FloatingType
    {
        Sinus,
        PerlinNoise
    }

    [SerializeField]
    EasingFunction.Ease colorsEase = EasingFunction.Ease.EaseInOutQuart;

    EasingFunction.Function _func;
    Vector3 _initialPos;
    float _value;
    float _funcedValue;

    void Float()
    {
        // get easing function value
        _value = data.micVolumeNormalized;
        _funcedValue = _func(0, 1, _value);

        switch (type) {
            case FloatingType.Sinus:
                float sin = Mathf.Sin(Time.frameCount * floatFrequency) * floatAmplitude;

                // lerp values with easing function
                Vector3 vec = transform.localPosition;
                vec.Set(_initialPos.x, _initialPos.y + sin * amplitude, _initialPos.z);
                transform.localPosition = vec;
                break;

            case FloatingType.PerlinNoise:
                // get easing function value
                _value = data.micVolumeNormalized;
                _funcedValue = _func(0, 1, _value);

                // Range over which height varies.
                float heightScale = floatAmplitude;

                // Distance covered per second along X axis of Perlin plane.
                float xScale = floatFrequency;

                float height = heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f);
                Vector3 pos = transform.localPosition;
                pos.y = height + _initialPos.y;
                transform.localPosition = pos;
                break;

            default:
                break;
        }
    
    }

    void Start()
    {
        _func = EasingFunction.GetEasingFunction(colorsEase);
        _initialPos = transform.localPosition;
    }

    void Update()
    {
        Float();
    }
}
