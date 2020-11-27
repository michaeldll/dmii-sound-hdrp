using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MusicTransitionCollider : MonoBehaviour
{
    [SerializeField]
    AudioMixerSnapshot snapshotActive;

    [SerializeField]
    AudioMixerSnapshot snapshotInactive;

    [SerializeField]
    AudioMixer mixer;

    [SerializeField]
    float transitionTime = 2.0f;

    public TransitionType type = TransitionType.ToActive;
    public enum TransitionType
    {
        ToActive,
        ToInactive
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(type == TransitionType.ToActive) snapshotActive.TransitionTo(transitionTime);
        else if (type == TransitionType.ToInactive) snapshotInactive.TransitionTo(transitionTime);
    }
}
