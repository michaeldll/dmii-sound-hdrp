using System;
using System.Collections;
using UnityEngine;

sealed class KeyboardPiano : MonoBehaviour
{
    [SerializeField] int transpose = 12;
    [SerializeField] AudioSource[] audioSources = null;
    [SerializeField] DataObject data;
    [SerializeField] float acceleration;
    private int _sourceIndex = 0;
    public float _accelerationFactor = 0;

    void GetNoteIndex(string note){
        switch (note)
        {
            case "C3":
                _sourceIndex = 0;
                break;

            case "D3":
                _sourceIndex = 1;
                break;

            case "E3":
                _sourceIndex = 2;
                break;

            case "F3":
                _sourceIndex = 3;
                break;

            case "G3":
                _sourceIndex = 4;
                break;

            case "A3":
                _sourceIndex = 5;
                break;

            case "B3":
                _sourceIndex = 6;
                break;

            case "C4":
                _sourceIndex = 7;
                break;

            default:
                break;
        }
    }

    void PlayNote(string note)
    {
        // Debug.Log(string.Format(
        //     "Note On #{0} ",
        //     note
        // ));


        GetNoteIndex(note);

        audioSources[_sourceIndex].pitch = Mathf.Pow(2, (1 + transpose) / 12);

        audioSources[_sourceIndex].Play();

        if(_accelerationFactor < 9) _accelerationFactor += 1;
    }

    void StopNote(string note)
    {
        // Debug.Log(string.Format(
        //     "Note Off #{0} ",
        //     note
        // ));


        GetNoteIndex(note);

        audioSources[_sourceIndex].Stop();

        if(_accelerationFactor < 9) _accelerationFactor -= 1;
    }

    void Update(){
        //keydown
        if (Input.GetKeyDown("a"))
        {
            PlayNote("C3");
        }
        if (Input.GetKeyDown("z"))
        {
            PlayNote("D3");
        }
        if (Input.GetKeyDown("e"))
        {
            PlayNote("E3");
        }
        if (Input.GetKeyDown("r"))
        {
            PlayNote("F3");
        }
        if (Input.GetKeyDown("t"))
        {
            PlayNote("G3");
        }
        if (Input.GetKeyDown("y"))
        {
            PlayNote("A3");
        }
        if (Input.GetKeyDown("u"))
        {
            PlayNote("B3");
        }
        if (Input.GetKeyDown("i"))
        {
            PlayNote("C4");
        }

        //keyup
        if (Input.GetKeyUp("a"))
        {
            StopNote("C3");
        }
        if (Input.GetKeyUp("z"))
        {
            StopNote("D3");
        }
        if (Input.GetKeyUp("e"))
        {
            StopNote("E3");
        }
        if (Input.GetKeyUp("r"))
        {
            StopNote("F3");
        }
        if (Input.GetKeyUp("t"))
        {
            StopNote("G3");
        }
        if (Input.GetKeyUp("y"))
        {
            StopNote("A3");
        }
        if (Input.GetKeyUp("u"))
        {
            StopNote("B3");
        }
        if (Input.GetKeyUp("i"))
        {
            StopNote("C4");
        }

        if(_accelerationFactor > 0 && data.micVolumeNormalized <= (1 - acceleration )) data.SetVolume(data.micVolumeNormalized + acceleration * _accelerationFactor);

        if(data.micVolumeNormalized > (acceleration / 2)) data.SetVolume(data.micVolumeNormalized - ( acceleration) / 2) ;
    }
}