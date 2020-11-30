using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LookAtEvent : UnityEvent<Transform>
{
}

public class LookCheckpoint : MonoBehaviour
{

    [SerializeField] Transform target;
    PlayerLookManager _manager;
    LookAtEvent _e;

    public enum LookType
    {
        Leave,
        Enter
    }

    private void Start()
    {
        if (_e == null)
            _e = new LookAtEvent();

        _e.AddListener(LookAt);
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        _manager = _player.GetComponent<PlayerLookManager>();
    }

    void LookAt(Transform target)
    {
        _manager.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") _e.Invoke(target);
    }
}
