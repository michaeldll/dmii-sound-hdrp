﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;

public class Door : MonoBehaviour
{
    // enum Animations
    // {
    //     ScaleX,
    //     ScaleY,
    //     MoveUp,
    //     Fade
    // };

    // [SerializeField]
    // private Animations _animation = Animations.ScaleX;

    [HideInInspector]
    public Transform transformOverride;

    [SerializeField]
    private Navigation _worldsNavigation = null;

    [SerializeField]
    private string _type = null;

    [SerializeField]
    private VoidEvent OnDoorTransitionIn = null;

    private Transform _player = null;
    private Transform _virtualPlayer = null;
    private CharacterController _playerCharacterController;

    private World _world;

    private GameObject _destinationWorldGameObject;
    private World _destinationWorld;
    private Transform _destinationDoorTransform;

    private bool _isVisible = false;

    private bool _isPlayerOverlapping = false;

    private PlayableDirector _timeline;

    // Public
    public void SetDestination(int id, int direction)
    {
        string destinationWorldName = "World_" + id.ToString();
        _destinationWorldGameObject = GameObject.Find(destinationWorldName);
        _destinationWorld = _destinationWorldGameObject.GetComponent<World>();
        _destinationDoorTransform = direction == -1 ? _destinationWorld.doorLeave.transformOverride : _destinationWorld.doorEnter.transformOverride;
    }

    public void TransitionIn()
    {
        Debug.Log(_isVisible);
        if (_isVisible) return;
        _isVisible = true;

        Transform portal = transform.Find("Portal");

        if (_timeline)
        {
            _timeline.Play();
        }
        else
        {
            portal.DOScale(new Vector3(1, 1, 1), 2f).SetEase(Ease.InOutSine);
        }
    }

    public void TransitionOut()
    {
        if (!_isVisible) return;
        _isVisible = false;

        Transform portal = transform.Find("Portal");

        if (!_timeline)
        {
            Vector3 scale = new Vector3(0, 1, 1);
            portal.DOScale(scale, 2f);
        }
        else
        {
            Vector3 pos = portal.localPosition;
            pos.y = -11f;
            portal.localPosition = pos;
        }
    }

    // Private
    private void InitAppearance()
    {
        Transform portal = transform.Find("Portal");

        if (!_timeline)
        {
            portal.localScale = new Vector3(0, 1, 1);
        }
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
        Vector3 playerOffsetFromDoor = _player.position - transformOverride.position;
        Vector3 targetPosition = _destinationDoorTransform.position + playerOffsetFromDoor;

        // Vector3 targetPosition = _virtualPlayer.position;
        Quaternion targetRotation = _virtualPlayer.rotation;

        // Handler Character controller issue
        _playerCharacterController.enabled = false;
        _playerCharacterController.transform.position = targetPosition;
        _player.rotation = targetRotation;
        _playerCharacterController.transform.rotation = targetRotation;
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
        _virtualPlayer = GameObject.FindGameObjectWithTag("Virtual Player").transform;
        _timeline = GetComponent<PlayableDirector>();
        transformOverride = transform.Find("Transform GO");

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
