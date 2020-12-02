using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VirtualPlayer : MonoBehaviour
{
    [SerializeField]
    private State _readyState = null;

    private Transform _player = null;
    private Camera _playerCamera = null;
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

    // private void RenderCamera(ScriptableRenderContext context, Camera camera)
    private void Update()
    {
        if (!_readyState.GetState) return;

        Transform inTransform = _doorActive.transformOverride;
        Transform outTransform = _doorDestination.transformOverride;

        _camera.projectionMatrix = _playerCamera.projectionMatrix;

        Vector3 relativePosition = inTransform.InverseTransformPoint(_player.transform.position);
        relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
        transform.position = outTransform.TransformPoint(relativePosition);

        Vector3 relativeRotation = inTransform.InverseTransformDirection(_player.transform.forward);
        relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
        transform.forward = outTransform.TransformDirection(relativeRotation);

        _head.localPosition = _playerHead.localPosition;

        _renderTextureMaterial.mainTexture = _camera.targetTexture;
    }

    // Hooks
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHead = _player.Find("Head");

        _head = transform.Find("Head");
        _camera = _head.GetComponentInChildren<Camera>();
        _playerCamera = _player.GetComponentInChildren<Camera>();
    }

    void Start()
    {
        CreateRenderTexture();
    }
}
