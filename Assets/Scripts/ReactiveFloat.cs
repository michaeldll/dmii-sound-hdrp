using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveFloat : MonoBehaviour
{
    [SerializeField]
    DataObject data;

    [SerializeField]
    float floatAmplitude = 0.2f;

    [SerializeField]
    float floatFrequency = 0.5f;

    [SerializeField]
    float maxModulo = 0.05f;

    [SerializeField]
    FloatingType type = FloatingType.PerlinNoise;
    public enum FloatingType
    {
        Sinus,
        PerlinNoise
    }

    Vector3 _initialPos;
    float volumeFloatAmplitude;
    float volumeFloatFrequency;
    private float _modulo = 0f;


    void Float()
    {
        if (data) {
            volumeFloatAmplitude = floatAmplitude * data.micVolumeNormalized + _modulo.Map(0, 1f, 0, maxModulo);
            volumeFloatFrequency = floatFrequency * data.micVolumeNormalized + _modulo.Map(0, 1f, 0, maxModulo);
        }

        switch (type)
        {
            case FloatingType.Sinus:
                float sin = Mathf.Sin(Time.frameCount * volumeFloatFrequency) * volumeFloatAmplitude;

                // lerp values with easing function
                Vector3 vec = transform.localPosition;
                vec.Set(_initialPos.x, _initialPos.y + sin, _initialPos.z);
                transform.localPosition = vec;
                break;

            case FloatingType.PerlinNoise:
                // Range over which height varies.
                float heightScale = volumeFloatAmplitude;

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
        _initialPos = transform.localPosition;
        _modulo = mod(transform.localPosition.z, 1f);
    }

    void Update()
    {
        Float();
    }

    //Private
    private float mod(float n, float m)
    {
        return ((n % m) + m) % m;
    }
}
