using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPlayer : MonoBehaviour
{
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
    void Awake() {
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
        Vector3 position = Vector3.zero;
        Vector3 playerOffsetFromDoor = _player.position - _doorActive.transform.position;
        position = _doorDestination.transform.position + playerOffsetFromDoor;

        transform.position = position;
        _head.localPosition = _playerHead.localPosition;
        _head.rotation = _playerHead.rotation;
        transform.rotation = _player.rotation;

        // Todo: See why we have to set the texture each frame 
        _renderTextureMaterial.mainTexture = _camera.targetTexture;
    }
}
