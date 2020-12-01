// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MicAnalyzer : MonoBehaviour
// {
//     void Start()
//     {
//         AudioSource audioSource = GetComponent<AudioSource>();
//         audioSource.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
//         audioSource.Play();
//     }

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    public DataObject data;
    public float micLoudnessinDecibels;
    public List<string> allDevices;
    public string device;
    public float micNormalized;
    public float amplitude = 10f;
    public float minimumLimitDb = -40f;

    private AudioClip _clipRecord;
    private AudioClip _recordedClip;
    private int _sampleWindow = 128;
    private float _micLoudness;
    private bool _isInitialized;

    //mic initialization
    // public void InitMic()
    // {
    //     foreach (string micDevice in Microphone.devices)
    //     {
    //         allDevices.Add(micDevice);
    //     }

    //     if (device == null)
    //     {
    //         device = Microphone.devices[0];
    //     }
    //     _clipRecord = Microphone.Start(device, true, 999, 44100);
    //     _isInitialized = true;
    // }

    // public void StopMicrophone()
    // {
    //     Microphone.End(device);
    //     _isInitialized = false;
    // }

    // //get data from microphone into audioclip
    // float MicrophoneLevelMax()
    // {
    //     float levelMax = 0;
    //     float[] waveData = new float[_sampleWindow];
    //     int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
    //     if (micPosition < 0) return 0;
    //     _clipRecord.GetData(waveData, micPosition);
    //     // Getting a peak on the last 128 samples
    //     for (int i = 0; i < _sampleWindow; i++)
    //     {
    //         float wavePeak = waveData[i] * waveData[i];
    //         if (levelMax < wavePeak)
    //         {
    //             levelMax = wavePeak;
    //         }
    //     }
    //     return levelMax;
    // }

    // //get data from microphone into audioclip
    // float MicrophoneLevelMaxDecibels()
    // {

    //     float db = 20 * Mathf.Log10(Mathf.Abs(_micLoudness));

    //     return db;
    // }

    // public float FloatLinearOfClip(AudioClip clip)
    // {
    //     StopMicrophone();

    //     _recordedClip = clip;

    //     float levelMax = 0;
    //     float[] waveData = new float[_recordedClip.samples];

    //     _recordedClip.GetData(waveData, 0);
    //     // Getting a peak on the last 128 samples
    //     for (int i = 0; i < _recordedClip.samples; i++)
    //     {
    //         float wavePeak = waveData[i] * waveData[i];
    //         if (levelMax < wavePeak)
    //         {
    //             levelMax = wavePeak;
    //         }
    //     }
    //     return levelMax;
    // }

    // public float DecibelsOfClip(AudioClip clip)
    // {
    //     StopMicrophone();

    //     _recordedClip = clip;

    //     float levelMax = 0;
    //     float[] waveData = new float[_recordedClip.samples];

    //     _recordedClip.GetData(waveData, 0);
    //     // Getting a peak on the last 128 samples
    //     for (int i = 0; i < _recordedClip.samples; i++)
    //     {
    //         float wavePeak = waveData[i] * waveData[i];
    //         if (levelMax < wavePeak)
    //         {
    //             levelMax = wavePeak;
    //         }
    //     }

    //     float db = 20 * Mathf.Log10(Mathf.Abs(levelMax));

    //     return db;
    // }



    // void Update()
    // {
    //     // levelMax equals to the highest normalized value power 2, a small number because < 1
    //     // pass the value to a static var so we can access it from anywhere
    //     _micLoudness = MicrophoneLevelMax();
    //     micLoudnessinDecibels = MicrophoneLevelMaxDecibels();
    //     if (micLoudnessinDecibels > minimumLimitDb)
    //     {
    //         micNormalized = Mathf.Clamp((1 / Mathf.Abs(micLoudnessinDecibels) * amplitude), 0, 1);
    //         data.SetVolume(micNormalized);
    //     }
    //     else
    //     {
    //         data.SetVolume(0f);
    //     }
    // }



    // // start mic when scene starts
    // void OnEnable()
    // {
    //     InitMic();
    //     _isInitialized = true;
    // }

    // //stop mic when loading a new level or quit application
    // // void OnDisable()
    // // {
    // //     StopMicrophone();
    // // }
 

    // // void OnDestroy()
    // // {
    // //     StopMicrophone();
    // // }
 
 


    // // make sure the mic gets started & stopped when application gets focused
    // void OnApplicationFocus(bool focus)
    // {
    //     if (focus)
    //     {
    //         //Debug.Log("Focus");

    //         if (!_isInitialized)
    //         {
    //             //Debug.Log("Init Mic");
    //             InitMic();
    //         }
    //     }
    //     if (!focus)
    //     {
    //         //Debug.Log("Pause");
    //         // StopMicrophone();
    //         //Debug.Log("Stop Mic");

        // }
    // }
}