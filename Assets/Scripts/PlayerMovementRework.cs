using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRework : MonoBehaviour
{
    [SerializeField]
    public LayerMask groundLayer;

    [SerializeField]
    public float moveSpeed = 10f;

    [SerializeField]
    public float rotationSpeed = 5f;

    [SerializeField]
    public float gravity = -9.81f;

    [SerializeField]
    public float headBobAmplitude = 0.5f;

    [SerializeField]
    public float headBobSpeed = 0.1f;

    [SerializeField]
    public float friction = -1f;

    [SerializeField]
    public DataObject data = null;

    [SerializeField]
    public MovementType movement = MovementType.KEYBOARD;
    public enum MovementType
    {
        SOUND,
        KEYBOARD
    }

    [SerializeField]
    public bool gameStartOnAwake = false;

    public Vector3 _velocity;
    public Vector3 _acceleration;
    public Vector3 _moveRotation;

    private Transform _head;
    private CharacterController _controller;
    private Transform _groundCheck;

    private float _groundDistance = 0.4f;
    private bool _isGrounded;
    private float _time = 0f;
    private Vector3 _startHeadPosition;


    // Public
    public void InitPosition(Vector3 startPosition)
    {
        transform.position = startPosition;
    }

    // Hooks
    void Awake() {
        _controller = GetComponent<CharacterController>();
        _head = transform.Find("Head");
        _groundCheck = transform.Find("Ground Check");

        data.SetVolume(0f);
        data.SetGameStarted(gameStartOnAwake);
    }

    void Start()
    {
        _startHeadPosition = new Vector3(_head.position.x, _head.position.y, _head.position.z);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, groundLayer);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        if(data && data.isGameStarted)
        {
            switch (movement)
            {
                case MovementType.KEYBOARD:
                    _acceleration.x = Input.GetAxis("Horizontal");
                    _acceleration.z = Input.GetAxis("Vertical");
                    break;

                case MovementType.SOUND:
                    _acceleration.z = data.micVolumeNormalized;
                    break;

                default:
                    Debug.LogError("Please specify a movement type");
                    break;
            }

        }

        // Head Bob
        float offsetY = Mathf.Sin(_time * headBobSpeed) * headBobAmplitude * _acceleration.z;
        _head.localPosition = new Vector3(0, _startHeadPosition.y + offsetY, 0);

        // Movements
        if (!_isGrounded) _acceleration.y += gravity * Time.deltaTime;

        _acceleration = transform.forward * _acceleration.z * moveSpeed * Time.deltaTime;
        _moveRotation = new Vector3(0, _acceleration.x, 0) * rotationSpeed * Time.deltaTime;

        _velocity = _acceleration;
        _controller.Move(_velocity);
        transform.Rotate(_moveRotation);

        // Not used but might be useful in the future
        //Vector3 direction = new Vector3(_acceleration.x, 0, _acceleration.z).normalized;
        //float targetAngle;
        //if (direction.magnitude > 0.1f)
        //{
        //    targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //}

        _acceleration *= friction;

        _time += 1f * Time.deltaTime;
    }


}
