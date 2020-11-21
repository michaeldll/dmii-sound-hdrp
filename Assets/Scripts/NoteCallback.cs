using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

// NoteCallback.cs - This script shows how to define a callback to get notified
// on MIDI note-on/off events.

sealed class NoteCallback : MonoBehaviour
{
    [SerializeField] int transpose = 12;
    [SerializeField] public AudioSource[] audioSources = null;
    private int sourceIndex = 0;

    void Start()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (change != InputDeviceChange.Added) return;

            var midiDevice = device as Minis.MidiDevice;
            if (midiDevice == null) return;

            midiDevice.onWillNoteOn += (note, velocity) =>
            {
                // Note that you can't use note.velocity because the state
                // hasn't been updated yet (as this is "will" event). The note
                // object is only useful to specify the target note (note
                // number, channel number, device name, etc.) Use the velocity
                // argument as an input note velocity.
                Debug.Log(string.Format(
                    "Note On #{0} ({1}) vel:{2:0.00} ",//ch:{3} dev:'{4}'",
                    note.noteNumber,
                    note.shortDisplayName,
                    velocity//,
                            //(note.device as Minis.MidiDevice)?.channel,
                            //note.device.description.product
                ));


                switch (note.shortDisplayName)
                {
                    case "C3":
                        sourceIndex = 0;
                        break;

                    case "D3":
                        sourceIndex = 1;
                        break;

                    case "E3":
                        sourceIndex = 2;
                        break;

                    case "F3":
                        sourceIndex = 3;
                        break;

                    case "G3":
                        sourceIndex = 4;
                        break;

                    case "A3":
                        sourceIndex = 5;
                        break;

                    case "B3":
                        sourceIndex = 6;
                        break;

                    default:
                        break;
                }

                audioSources[sourceIndex].pitch = Mathf.Pow(2, (1 + transpose) / 12);

                audioSources[sourceIndex].Play();
            };

            midiDevice.onWillNoteOff += (note) =>
            {
                Debug.Log(string.Format(
                    "Note Off #{0} ({1}) ch:{2} dev:'{3}'",
                    note.noteNumber,
                    note.shortDisplayName,
                    (note.device as Minis.MidiDevice)?.channel,
                    note.device.description.product
                ));

                audioSources[sourceIndex].Stop();
            };
        };
    }
}