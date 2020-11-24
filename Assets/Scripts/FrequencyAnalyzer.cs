using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyAnalyzer : MonoBehaviour
{
    [SerializeField] AudioSource source;

    [SerializeField] GameObject prefab;
	[SerializeField] GameObject parent;
	[SerializeField, Range(1,512)] int samples = 512;
    [SerializeField, Range(1,20)] int radius = 10;
    [SerializeField] float scale = 80;
    [SerializeField, Range(0,1)] float lerping = 0.85f;
    [SerializeField] float particleThreshold = 0.01f;
    private float[] _samples;
    private GameObject[] _bars;
    private ParticleSystem[] _particles;
    
    void Start()
    {
        _samples = new float[samples];
        _bars = new GameObject[samples];
        _particles = new ParticleSystem[samples];

        GameObject bar;
        float a = (Mathf.PI*2)/samples;
        prefab.SetActive(true);
        
        for (int i = 0; i < _samples.Length; i++)
        {
            bar = Instantiate(prefab, parent.transform);
            bar.transform.localPosition = new Vector3(Mathf.Sin(a*i)*radius, 0,Mathf.Cos(a*i)*radius);

            _bars[i] = bar;
            _particles[i] = bar.GetComponentInChildren<ParticleSystem>();
            _particles[i].transform.localPosition = new Vector3(Mathf.Sin(a*i)*radius, 0,Mathf.Cos(a*i)*radius);
        }
        prefab.SetActive(false);
    }


    void Update()
    {
        source.GetSpectrumData(_samples, 1, FFTWindow.Blackman);
        GameObject bar;
        Vector3 scale;
        Vector3 prev;

        for (int i = 0; i < _samples.Length; i++)
        {
            bar =  _bars[i];
            prev = scale = bar.transform.localScale;
            scale.y = Mathf.Clamp(_samples[i] * this.scale, 0, 1);
            bar.transform.localScale = Vector3.Lerp(scale, prev,lerping);

            if(_samples[i] > particleThreshold){
                _particles[i].Play();
            }
        }
    }
}
