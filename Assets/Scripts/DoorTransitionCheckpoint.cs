using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class DoorTransitionCheckpoint : MonoBehaviour
{
    private VoidEvent OnDoorTransitionIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnDoorTransitionIn.e.Invoke();
            OnDoorTransitionIn.e.RemoveAllListeners();
        }
    }
}
