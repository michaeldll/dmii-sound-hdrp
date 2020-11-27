using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteAnalyzer : MonoBehaviour
{
    /**
        Analyzer
    */
    public AudioSource audioSource;
    public FFTWindow window = FFTWindow.BlackmanHarris;
    public int nSamples = 1024;

    private float fMax;
    private float fMin;
    private float fSample;
    private float threshold = 0.02f;
    private List<string> notes = new List<string>();
    private string[] notesArray = new string[12];

    public int currentNodeIndex = 0;

    /**
        Visualizer
    */
    // public GameObject cube;
    // public float padding = 0.1f;
    // public int size = 10;
    // public float scale = 10f;
    // public Vector3 minScale = new Vector3(1, 50, 50);
    // public float lerpValue = 0.01f;

    // private List<GameObject> cubes = new List<GameObject>();


    void Start()
    {
        fSample = AudioSettings.outputSampleRate;
        fMax = fSample / 2;
        fMin = fMax / nSamples;

        setupNotes();
    }

    void Update()
    {
        float[] spectrum = new float[nSamples];
        AudioListener.GetSpectrumData(spectrum, 0, window);

        float pitch = analyzeSpectrum(spectrum);
        string note = getNoteFromPitch(pitch);

        // Debug.Log(string.Concat(pitch, " Hz"));
        // Debug.Log(note);
        currentNodeIndex = getNoteIndexFromPitch(pitch);
    }

    /**
        Analyzer
    */
    private void setupNotes()
    {
        notes.Add("C");
        notes.Add("C#");
        notes.Add("D");
        notes.Add("D#");
        notes.Add("E");
        notes.Add("F");
        notes.Add("F#");
        notes.Add("G");
        notes.Add("G#");
        notes.Add("A");
        notes.Add("A#");
        notes.Add("B");

        notesArray = notes.ToArray();
    }

    private float analyzeSpectrum(float[] spectrum)
    {
        float maxV = 0;
        int maxN = 0;

        for (int i = 0; i < nSamples; i++)
        {
            // find max 
            if (spectrum[i] > maxV && spectrum[i] > threshold)
            {
                maxV = spectrum[i];
                maxN = i; // maxN is the index of max
            }
        }

        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < nSamples - 1)
        {
            // interpolate index using neighbours
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }

        float pitchValue = freqN * (fSample / 2) / nSamples; // convert index to frequencys

        return pitchValue;
    }

    private string getNoteFromPitch(float pitch)
    {
        float noteNum = 12f * (Mathf.Log(pitch / 440f) / Mathf.Log(2f));
        float A0 = 27.5f;
        int octave = 0;
        float octavePitch = 0f;
        

        while (pitch > octavePitch)
        {
            octave++;
            octavePitch = A0 * Mathf.Pow(2, octave);
        }

        int index = (Mathf.RoundToInt(noteNum) + 69) % notesArray.Length;

        if (index < 0)
        {
            return "";
        }

        return notes[index];
    }
    
    private int getNoteIndexFromPitch(float pitch)
    {
        float noteNum = 12f * (Mathf.Log(pitch / 440f) / Mathf.Log(2f));
        float A0 = 27.5f;
        int octave = 0;
        float octavePitch = 0f;
        

        while (pitch > octavePitch)
        {
            octave++;
            octavePitch = A0 * Mathf.Pow(2, octave);
        }

        int index = (Mathf.RoundToInt(noteNum) + 69) % notesArray.Length;

        if (index < 0)
        {
            return 0;
        }

        return index;
    }


    /**
        Visualizer
    */

}