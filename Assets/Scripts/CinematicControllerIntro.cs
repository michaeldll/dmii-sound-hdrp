using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControllerIntro : MonoBehaviour
{
    [HideInInspector]
    public double duration;

    private PlayableDirector _introCinematicDirector;

    // Public
    public void PlayCinematic()
    {
        if (!_introCinematicDirector) return;
        _introCinematicDirector.Play();
    }

    public void Reset()
    {
        // Maybe do stuff
    }

    public void End()
    {
        _introCinematicDirector.initialTime = duration;
        _introCinematicDirector.Play();
    }

    // Hooks
    void Awake()
    {
        _introCinematicDirector = GetComponentInChildren<PlayableDirector>();
        duration = _introCinematicDirector ? _introCinematicDirector.duration : 0d;
    }
}
