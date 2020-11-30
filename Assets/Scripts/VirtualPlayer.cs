using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPlayer : MonoBehaviour
{
    [SerializeField]
    private State _readyState = null;

    private Transform _player = null;
    private Transform _playerHead;
    private Camera _camera;

    [SerializeField]
    private Material _renderTextureMaterial = null;

    private Transform _head;

    // Destination
    private GameObject _worldDestinationGameObject;
    private World _worldDestination;
    private Transform _worldDestinationTransform;
    private Door _doorDestination;

    // Active
    private GameObject _worldActiveGameObject;
    private World _worldActive;
    private Transform _worldActiveTransform;
    private Door _doorActive;

    // Public
    public void SetActiveWorld(int id)
    {
        string worldName = "World_" + id.ToString();
        _worldActiveGameObject = GameObject.Find(worldName);
        _worldActive = _worldActiveGameObject.GetComponent<World>();
        _worldActiveTransform = _worldActiveGameObject.GetComponent<World>().transform;
        _doorActive = _worldActive.doorLeave;
    }

    public void SetDestination(int id)
    {
        string worldName = "World_" + id.ToString();
        _worldDestinationGameObject = GameObject.Find(worldName);
        _worldDestination = _worldDestinationGameObject.GetComponent<World>();
        _worldDestinationTransform = _worldDestinationGameObject.GetComponent<World>().transform;
        _doorDestination = _worldDestination.doorEnter;
    }

    // Private
    private void CreateRenderTexture()
    {
        if (_camera.targetTexture != null)
        {
            _camera.targetTexture.Release();
        }

        Material newMaterial = new Material(_renderTextureMaterial);

        _camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        _camera.targetTexture.Create();

        _renderTextureMaterial.mainTexture = _camera.targetTexture;
        newMaterial.mainTexture = _camera.targetTexture;
    }

    // Hooks
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHead = _player.Find("Head");

        _head = transform.Find("Head");
        _camera = _head.GetComponentInChildren<Camera>();
    }

    void Start()
    {
        CreateRenderTexture();
    }

    void Update()
    {
        if (!_readyState.GetState) return;

        Vector3 playerOffsetFromDoor = _player.position - _doorActive.transform.position;

        // Get Real distance from Player to Door --> Magnitude
        float playerDistanceFromDoor = playerOffsetFromDoor.magnitude;
        // Get direction from Player to Door --> Normalized vector
        Vector3 playerToDoorDirection = playerOffsetFromDoor.normalized;

        // Door Active direction
        Vector3 DoorActiveDirection = _doorActive.transform.forward;
        // Door Destination direction
        Vector3 DoorDestinationDirection = _doorDestination.transform.forward;

        // Get Offset du to doors respective rotations
        Vector3 offsetBetweenDoors = -DoorDestinationDirection * playerDistanceFromDoor - DoorActiveDirection * playerDistanceFromDoor;

        Vector3 position = _doorDestination.transform.position + playerOffsetFromDoor + offsetBetweenDoors;

        Quaternion offsetRotation = Quaternion.FromToRotation(_doorActive.transform.forward, -_doorDestination.transform.forward);
        Quaternion rotation = _player.rotation * offsetRotation;

        // Apply Positions / Rotations
        transform.position = position;
        transform.rotation = rotation;
        _head.localPosition = _playerHead.localPosition;

        // Todo: See why we have to set the texture each frame 
        _renderTextureMaterial.mainTexture = _camera.targetTexture;

        // If Rotation DoorActive  0, 0, 0 --> This works
        // transform.rotation = _player.rotation * _doorDestination.transform.rotation;
    }

    void OnDrawGizmos() {
        if (!_doorActive || !_doorDestination) return;

        Vector3 playerOffsetFromDoor = _player.position - _doorActive.transform.position;

        // Get Real distance from Player to Door --> Magnitude
        float playerDistanceFromDoor = playerOffsetFromDoor.magnitude;
        // Get direction from Player to Door --> Normalized vector
        Vector3 playerToDoorDirection = playerOffsetFromDoor.normalized;

        // Door Active direction
        Vector3 DoorActiveDirection = _doorActive.transform.forward;
        // Door Destination direction
        Vector3 DoorDestinationDirection = _doorDestination.transform.forward;

        // Get Offset du to doors respective rotations
        Vector3 offsetBetweenDoors = -DoorDestinationDirection * playerDistanceFromDoor - DoorActiveDirection * playerDistanceFromDoor;

        Vector3 position = _doorDestination.transform.position + playerOffsetFromDoor + offsetBetweenDoors;

        // Draw Guizmos Player
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(DoorActiveDirection * playerDistanceFromDoor, 3);

        // Draw Guizmos Relative Virtual Player
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(-DoorDestinationDirection * playerDistanceFromDoor, 3);

        // Draw Guizmos Virtual Player
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(position, 3);
        Gizmos.DrawLine(position, position + _player.forward * 5f);
    }
}
