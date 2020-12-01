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
    FloatingType type = FloatingType.PerlinNoise;
    public enum FloatingType
    {
        Sinus,
        PerlinNoise
    }

    Vector3 _initialPos;
    float _randFloat1;
    float _randFloat2;

    void Float()
    {
        float volumeFloatAmplitude;
        if (data) volumeFloatAmplitude = floatAmplitude * data.micVolumeNormalized + _randFloat1;
        else volumeFloatAmplitude = _randFloat2;

        switch (type)
        {
            case FloatingType.Sinus:
                float sin = Mathf.Sin(Time.frameCount * floatFrequency) * volumeFloatAmplitude;

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
        _randFloat1 = Random.Range(0f, 0.1f);
        _randFloat2 = Random.Range(0f, 0.2f);
        _initialPos = transform.localPosition;
    }

    void Update()
    {
        Float();
    }
}
