using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class FrequencyViz : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] GameObject prefabToClone;
    [SerializeField] GameObject parentToPrefabClones;
    [SerializeField, Range(1, 512)] int samples = 100;
    [SerializeField, Range(1, 20)] int radius = 10;
    [SerializeField] float scale = 80;
    [SerializeField, Range(0, 1)] float lerping = 0.85f;
    [SerializeField] float particleThreshold = 0.5f;

    private float[] _samples;
    private GameObject[] _bars;
    private ParticleSystem[] _particles;

    void Start()
    {
        _samples = new float[samples];
        _bars = new GameObject[samples];
        _particles = new ParticleSystem[samples];

        GameObject bar;
        float a = (Mathf.PI * 2) / samples;
        prefabToClone.SetActive(true);

        for (int i = 0; i < _samples.Length; i++)
        {
            bar = Instantiate(prefabToClone, parentToPrefabClones.transform);
            bar.transform.position = new Vector3(Mathf.Sin(a * i) * radius, 0, Mathf.Cos(a * i) * radius);

            _bars[i] = bar;
            _particles[i] = bar.GetComponentInChildren<ParticleSystem>();
            _particles[i].transform.position = new Vector3(Mathf.Sin(a * i) * radius, 0, Mathf.Cos(a * i) * radius);
        }
        prefabToClone.SetActive(false);
    }

    void Update()
    {
        source.GetSpectrumData(_samples, 1, FFTWindow.Blackman);
        GameObject bar;
        Vector3 scale;
        Vector3 prev;
        //with scale
        for (int i = 0; i < _samples.Length; i++)
        {
            bar = _bars[i];
            prev = scale = bar.transform.localScale;
            scale.y = _samples[i] * this.scale;
            bar.transform.localScale = Vector3.Lerp(scale, prev, lerping);

            if (_samples[i] > particleThreshold)
            {
                _particles[i].Play();
            }
        }
    }
}
