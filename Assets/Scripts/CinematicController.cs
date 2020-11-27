using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicController : MonoBehaviour
{
    public double duration;

    private PlayableDirector _introCinematicDirector;

    // Public
    public void PlayCinematic()
    {
        _introCinematicDirector.Play();
    }

    public void Reset()
    {
        // Maybe do stuff
    }

    // Hooks
    void Awake()
    {
        _introCinematicDirector = GetComponentInChildren<PlayableDirector>();
        duration = _introCinematicDirector.duration;
    }
}
