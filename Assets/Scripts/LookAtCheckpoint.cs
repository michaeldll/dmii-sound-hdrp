using UnityEngine;

public class LookAtCheckpoint : MonoBehaviour
{
    public GameEvent onEnterCheckpoint;
    public GameEvent onLeaveCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") onEnterCheckpoint.Raise();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") onLeaveCheckpoint.Raise();
    }
}
