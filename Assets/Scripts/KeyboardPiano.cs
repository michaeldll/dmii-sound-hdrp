#pragma warning disable 0649

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class KeyboardPiano : MonoBehaviour
{
    [SerializeField] int transpose = 12;
    [SerializeField] AudioSource[] audioSources = null;
    // [SerializeField] List<AudioSource> audioSources = new List<AudioSource>; //TODO: improve with List
    [SerializeField] List<string> keys = new List<string>() {
        "a", "z", "e", "r", "t", "y", "u", "o", "p", "q", "w"
    }; //TODO: improve with List
    [SerializeField] DataObject data;
    [SerializeField] float acceleration;
    public float _accelerationFactor = 0;
    public KeyBoardSchema schema = KeyBoardSchema.French;
    public enum KeyBoardSchema {
        French,
        English
    }

    void PlayNote(int index)
    {
        // Debug.Log(string.Format(
        //     "Note On #{0} ",
        //     note
        // ));

        audioSources[index].pitch = Mathf.Pow(2, (1 + transpose) / 12);

        audioSources[index].Play();

        if (_accelerationFactor < audioSources.Length) _accelerationFactor += 1;
    }

    void Update()
    {
        //keydown
        if (Input.GetKeyDown("a"))
        {
            if(schema == KeyBoardSchema.French) PlayNote(0);
            else if(schema == KeyBoardSchema.English) PlayNote(10);
        }
        if (Input.GetKeyDown("z"))
        {
            if(schema == KeyBoardSchema.French) PlayNote(1);
        }
        if (Input.GetKeyDown("w"))
        {
            if(schema == KeyBoardSchema.English) PlayNote(1);
        }
        if (Input.GetKeyDown("e"))
        {
            PlayNote(2);
        }
        if (Input.GetKeyDown("r"))
        {
            PlayNote(3);
        }
        if (Input.GetKeyDown("t"))
        {
            PlayNote(4);
        }
        if (Input.GetKeyDown("y"))
        {
            PlayNote(5);
        }
        if (Input.GetKeyDown("u"))
        {
            PlayNote(6);
        }
        if (Input.GetKeyDown("i"))
        {
            PlayNote(7);
        }
        if (Input.GetKeyDown("o"))
        {
            PlayNote(8);
        }
        if (Input.GetKeyDown("p"))
        {
            PlayNote(9);
        }
        if (Input.GetKeyDown("q"))
        {
            if(schema == KeyBoardSchema.French) PlayNote(10);
            else if(schema == KeyBoardSchema.English) PlayNote(0);
        }

        for(int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKeyUp(keys[i]))
            {
                _accelerationFactor -= 1;
            }
        }

        if (Input.anyKey == false) _accelerationFactor = 0;

        if (_accelerationFactor > 0 && data.micVolumeNormalized <= 1) data.SetVolume(data.micVolumeNormalized + acceleration * _accelerationFactor);

        else data.SetVolume(data.micVolumeNormalized - acceleration);
    }
}