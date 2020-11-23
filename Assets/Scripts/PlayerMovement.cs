#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerMovement : MonoBehaviour
{
    enum Modes
    {
        FreeMoveWithControls,
        FreeMoveWithSound,
        MoveAlongPathWithControls,
        MoveAlongPathWithSound
    };

    [SerializeField]
    private Modes _movementMode = Modes.FreeMoveWithControls;

    [SerializeField]
    private PlayerSettingsManager _settingsManager;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private float _gravity = -9.81f;

    [SerializeField]
    private float _rotationLerpValue = 0.01f;

    [SerializeField]
    private DataObject data = null;

    [SerializeField]
    private bool gameStartOnAwake = true;

    private PlayerSettings _settings;

    private float _speed = 10f;
    private float _rotationSpeed = 5f;
    private float _headBobAmplitude = 0.5f;
    private float _headBobSpeed = 0.1f;

    private PathCreator _pathCreator;
    private Transform _head;
    private CharacterController _controller;
    private Transform _groundCheck;
    private Vector3 _velocity;
    private float _groundDistance = 0.4f;
    private bool _isGrounded;
    private float _time = 0f;
    private Vector3 _startHeadPosition;
    private float _progress = 0f;
    private Quaternion _rotation;
    private Quaternion _rotationTarget;

    // Public
    public void InitPosition(Vector3 startPosition)
    {
        transform.position = startPosition;
    }

    public void SetActivePath(PathCreator path)
    {
        _pathCreator = path;
    }

    public void ResetProgress()
    {
        _progress = 0f;
    }

    // Private
    private void HandleMove()
    {
        switch (_movementMode)
        {
            case Modes.FreeMoveWithControls :
                FreeMoveWithControls();
                break;
            case Modes.FreeMoveWithSound :
                FreeMoveWithSound();
                break;
            case Modes.MoveAlongPathWithControls :
                MoveAlongPathWithControls();
                break;
            case Modes.MoveAlongPathWithSound :
                MoveAlongPathWithSound();
                break;
        }
    }

    private void SetupSettings()
    {
        switch (_movementMode)
        {
            case Modes.FreeMoveWithControls :
                _settings = _settingsManager.freeMoveWithControls;
                break;
            case Modes.FreeMoveWithSound :
                _settings = _settingsManager.freeMoveWithSound;
                break;
            case Modes.MoveAlongPathWithControls :
                _settings = _settingsManager.moveAlongPathWithControls;
                break;
            case Modes.MoveAlongPathWithSound :
                _settings = _settingsManager.moveAlongPathWithSound;
                break;
        }

        _speed = _settings.speed;
        _rotationSpeed = _settings.rotationSpeed;
        _headBobAmplitude = _settings.headBobAmplitude;
        _headBobSpeed = _settings.headBobSpeed;
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
        Vector3 _acceleration = new Vector3();
        _acceleration.z = data.micVolumeNormalized;

        // Head Bob
        float offsetY = Mathf.Sin(_time * _headBobSpeed) * _headBobAmplitude * _acceleration.z;
        _head.localPosition = new Vector3(0, _startHeadPosition.y + offsetY, 0);

        //Movements
        Vector3 move = transform.forward * _acceleration.z;

        _controller.Move(move * _speed * Time.deltaTime);
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

        _rotationTarget = transform.rotation;
        _rotationTarget.y = _pathCreator.path.GetRotation(_progress).y;

        _rotation.x = _rotationTarget.x;
        _rotation.z = _rotationTarget.z;
        _rotation.y = Lerp(_rotation.y, _rotationTarget.y, _rotationLerpValue);
        // _rotation.y = _pathCreator.path.GetRotation(_progress).y;

        // Head Bob
        float offsetY = Mathf.Sin(_time * _headBobSpeed) * _headBobAmplitude * z;
        _head.localPosition = new Vector3(0, _startHeadPosition.y + offsetY, 0);

        // Apply direct position and rotation to player controller
        _controller.enabled = false;
        _controller.transform.position = position;
        _controller.transform.rotation = _rotation;
        _controller.enabled = true;
    }

    private float Lerp(float start, float end, float value)
    {
        return (1f - value) * start + value * end;
    }

    private void MoveAlongPathWithSound()
    {
        Vector3 _acceleration = new Vector3();
        _acceleration.z = data.micVolumeNormalized;

        // Update progress from 0 to almost 1
        _progress += _acceleration.z * _speed * Time.deltaTime;
        _progress = Mathf.Clamp(_progress, 0f, 0.999f);

        // Use x and z position from path but preserve y
        Vector3 position = transform.position;
        position.x = _pathCreator.path.GetPointAtTime(_progress).x;
        position.z = _pathCreator.path.GetPointAtTime(_progress).z;

        _rotationTarget = transform.rotation;
        _rotationTarget.y = _pathCreator.path.GetRotation(_progress).y;

        _rotation.x = _rotationTarget.x;
        _rotation.z = _rotationTarget.z;
        _rotation.y = Lerp(_rotation.y, _rotationTarget.y, _rotationLerpValue);
        // _rotation.y = _pathCreator.path.GetRotation(_progress).y;

        // Head Bob
        float offsetY = Mathf.Sin(_time * _headBobSpeed) * _headBobAmplitude * _acceleration.z;
        _head.localPosition = new Vector3(0, _startHeadPosition.y + offsetY, 0);

        // Apply direct position and rotation to player controller
        _controller.enabled = false;
        _controller.transform.position = position;
        _controller.transform.rotation = _rotation;
        _controller.enabled = true;
    }

    // Hooks
    void Awake()
    {
        SetupSettings();
        _controller = GetComponent<CharacterController>();
        _head = transform.Find("Head");
        _groundCheck = transform.Find("Ground Check");
        _rotation = transform.rotation;
        _rotationTarget = transform.rotation;
        data.SetVolume(0f);
        data.SetGameStarted(gameStartOnAwake);
    }

    void Update()
    {
        // Useful to be able to edit settings on play mode
        SetupSettings();

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
