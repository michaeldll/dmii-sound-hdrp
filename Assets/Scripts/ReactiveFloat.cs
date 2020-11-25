using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveFloat : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    DataObject data;

    [SerializeField]
    float amplitude = 1;

    [SerializeField]
    float value;

    [SerializeField]
    float funcedValue;

    [SerializeField]
    float floatAmplitude = 1f;

    [SerializeField]
    float floatFrequency = 1f;

    // [SerializeField]
    // Vector3 direction = new Vector3(1, 2, 1);

    [SerializeField]
    EasingFunction.Ease colorsEase = EasingFunction.Ease.EaseInOutQuart;
    EasingFunction.Function _func;
    Vector3 _initialPos;
    [SerializeField]

    bool withSinus = false;

    void Start()
    {
        _func = EasingFunction.GetEasingFunction(colorsEase);
        _initialPos = target.localPosition;
    }

    void Float()
    {
        // "Bobbing" animation from 1D Perlin noise.
        if (!withSinus)
        {
            // Range over which height varies.
            float heightScale = 0.5f;

            // Distance covered per second along X axis of Perlin plane.
            float xScale = 0.5f;

            float height = heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f);
            Vector3 pos = transform.localPosition;
            pos.y = height + _initialPos.y;
            transform.localPosition = pos;
        }
        else
        {
            //WITH SINUS
            float sin = Mathf.Sin(Time.frameCount * floatFrequency) * floatAmplitude;

            // get easing function value
            value = data.micVolumeNormalized;
            funcedValue = _func(0, 1, value);

            // lerp values with easing function
            Vector3 vec = target.localPosition;
            vec.Set(_initialPos.x, _initialPos.y + sin * amplitude, _initialPos.z);
            target.localPosition = vec;
        }
    }

    void Update()
    {
        Float();
    }
}
