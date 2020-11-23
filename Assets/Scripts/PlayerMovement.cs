using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

#pragma warning disable 0649

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private float _rotationSpeed = 5f;

    [SerializeField]
    private float _gravity = -9.81f;

    [SerializeField]
    private float _headBobAmplitude = 0.5f;

    [SerializeField]
    private float _headBobSpeed = 0.1f;

    [SerializeField]
    private PathCreator _pathCreator;

    enum Modes
    {
        freeMoveWithControls,
        freeMoveWithSound,
        moveAlongPathWithControls,
        moveAlongPathWithSound
    };

    [SerializeField]
    private Modes _movementMode = Modes.freeMoveWithControls;

    private Transform _head;
    private CharacterController _controller;
    private Transform _groundCheck;
    private Vector3 _velocity;
    private float _groundDistance = 0.4f;
    private bool _isGrounded;
    private float _time = 0f;
    private Vector3 _startHeadPosition;
    private float _progress = 0f;

    // Public
    public void InitPosition(Vector3 startPosition)
    {
        transform.position = startPosition;
    }

    private void HandleMove()
    {
        if (_movementMode == Modes.freeMoveWithControls)
        {
            FreeMoveWithControls();
        }
        else if (_movementMode == Modes.freeMoveWithSound)
        {
            FreeMoveWithSound();
        }
        else if (_movementMode == Modes.moveAlongPathWithControls)
        {
            MoveAlongPathWithControls();
        }
        else if (_movementMode == Modes.moveAlongPathWithSound)
        {
            MoveAlongPathWithSound();
        }
    }

    private void FreeMoveWithControls()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Head Bob
        float offsetY = Mathf.Sin(_time * _headBobSpeed) * _headBobAmplitude * z;
        _head.localPosition = new Vector3(0, _startHeadPosition.y + offsetY, 0);

        // Movements
        Vector3 move = transform.forward * z;
        Vector3 moveDirection = new Vector3(0, x, 0);

        _controller.Move(move * _speed * Time.deltaTime);
        transform.Rotate(moveDirection * _rotationSpeed * Time.deltaTime);
    }

    private void FreeMoveWithSound()
    {

    }

    private void MoveAlongPathWithControls()
    {
        float z = Input.GetAxis("Vertical");

        // Update progress from 0 to almost 1
        _progress += z * _speed * Time.deltaTime;
        _progress = Mathf.Clamp(_progress, 0f, 0.999f);

        // Use x and z position from path but preserve y
        Vector3 position = transform.position;
        position.x = _pathCreator.path.GetPointAtTime(_progress).x;
        position.z = _pathCreator.path.GetPointAtTime(_progress).z;

        Quaternion rotation = transform.rotation;
        rotation.y = _pathCreator.path.GetRotation(_progress).y;

        // Head Bob
        float offsetY = Mathf.Sin(_time * _headBobSpeed) * _headBobAmplitude * z;
        _head.localPosition = new Vector3(0, _startHeadPosition.y + offsetY, 0);

        // Apply direct position and rotation to player controller
        _controller.enabled = false;
        _controller.transform.position = position;
        _controller.transform.rotation = rotation;
        _controller.enabled = true;
    }

    private void MoveAlongPathWithSound()
    {

    }

    // Hooks
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _head = transform.Find("Head");
        _groundCheck = transform.Find("Ground Check");
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundLayer);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        HandleMove();

        // Gravity
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        _time += 1f * Time.deltaTime;
    }

    void Start()
    {
        _startHeadPosition = new Vector3(_head.position.x, _head.position.y, _head.position.z);
    }
}
