using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class DoorTransitionCheckpoint : MonoBehaviour
{
    [SerializeField]
    private VoidEvent OnDoorTransitionIn;

    [SerializeField]
    private bool isBeginningDoor = false;

    [SerializeField]
    private Door beginningDoor;

    private delegate void TimeoutCallback();
    private bool _hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_hasTriggered)
        {
            if (!isBeginningDoor)
            {
                OnDoorTransitionIn.e.Invoke();
                OnDoorTransitionIn.e.RemoveAllListeners();
            }
            else
            {
                beginningDoor.TransitionIn();
            }
            _hasTriggered = true;
            StartCoroutine(SetTimeout(5f, () => { _hasTriggered = false; }));
        }
    }

    // Utils
    IEnumerator SetTimeout(float time, TimeoutCallback Method)
    {
        yield return new WaitForSeconds(time);

        Method();
    }
}
