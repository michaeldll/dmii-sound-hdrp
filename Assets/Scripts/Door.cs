using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Navigation _worldsNavigation = null;

    [SerializeField]
    private string _type = null;

    private Transform _player = null;
    private CharacterController _playerCharacterController;

    private World _world;

    private GameObject _destinationWorldGameObject;
    private World _destinationWorld;
    private Transform _destinationDoorTransform;

    private bool _isVisible = false;

    private bool _isPlayerOverlapping = false;

    // Public
    public void SetDestination(int id, int direction)
    {
        string destinationWorldName = "World_" + id.ToString();
        Debug.Log(destinationWorldName);
        _destinationWorldGameObject = GameObject.Find(destinationWorldName);
        Debug.Log(_destinationWorldGameObject);
        _destinationWorld = _destinationWorldGameObject.GetComponent<World>();
        Debug.Log(_destinationWorld);
        _destinationDoorTransform = direction == -1 ? _destinationWorld.doorLeave.transform : _destinationWorld.doorEnter.transform;
    }

    public void TransitionIn()
    {
        if (_isVisible) return;
        _isVisible = true;

        Transform portal = transform.Find("Portal");
        Vector3 scale = new Vector3(1, 1, 1);
        portal.DOScale(scale, 1f);
    }

    public void TransitionOut()
    {
        if (!_isVisible) return;
        _isVisible = false;

        Transform portal = transform.Find("Portal");
        Vector3 scale = new Vector3(0, 1, 1);
        portal.DOScale(scale, 1f);
    }

    // Private
    private void InitAppearance()
    {
        Transform portal = transform.Find("Portal");
        Vector3 scale = new Vector3(0, 1, 1);
        portal.localScale = scale;
    }

    private void CheckTrigger()
    {
        if (_isPlayerOverlapping)
        {
            float angle = Vector3.Angle(transform.forward, _player.transform.forward);

            if (angle > 90f)
            {
                TeleportPlayer();
                _isPlayerOverlapping = false;
            }
        }
    }

    private void TeleportPlayer()
    {
        Vector3 targetPosition = _destinationDoorTransform.position;
        targetPosition.y = _player.position.y;
        targetPosition.x = _player.position.x;

        // Handler Character controller issue
        _playerCharacterController.enabled = false;
        _playerCharacterController.transform.position = targetPosition;
        _playerCharacterController.enabled = true;

        // Set Navigation Data
        _worldsNavigation.Next();

        // Leave current world
        _world.Leave();

        // Enter next world
        _destinationWorld.Enter();
    }

    private void DisableCollision()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.enabled = false;
    }

    // Hooks
    void Awake()
    {
        _world = GetComponentInParent<World>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerCharacterController = _player.GetComponentInChildren<CharacterController>();

        if (_type == "")
        {
            _type = this.name.Split(char.Parse(" "))[1].ToLower();
        }
    }

    void Start()
    {
        if (_type == "enter")
        {
            DisableCollision();
        }

        InitAppearance();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerOverlapping = false;
        }
    }

    void Update()
    {
        CheckTrigger();
    }
}
